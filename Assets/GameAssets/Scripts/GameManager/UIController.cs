using DG.Tweening;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDoc;
    [SerializeField] private Button _openMenu;
    [SerializeField] private RectTransform _mainMenu;
    [SerializeField] private RectTransform _mainMenuBTN;
    [SerializeField] private RectTransform _killEffectsBTN;
    private int toggle;

    private void Start()
    {
        var rootVisualElement = _uiDoc.rootVisualElement;
        _openMenu = rootVisualElement.Q<Button>("mainmenu-button");
        _openMenu.clicked += OpenMainMenu;
        toggle = 1;
    }

    private void OpenMainMenu()
    {
        switch (toggle)
        {
            case 0:
                ++toggle;
                _openMenu.SetEnabled(false);
                _mainMenu.DOAnchorPosY(-15f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    GameManager.Instance.ChangeState(GameStatesEnum.Play);
                    _openMenu.SetEnabled(true);
                });
                break;
            case 1:
                GameManager.Instance.ChangeState(GameStatesEnum.Pause);
                --toggle;
                _openMenu.SetEnabled(false);
                _mainMenu.DOAnchorPosY(0f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    _openMenu.SetEnabled(true);
                });
                break;
        }
    }
}
