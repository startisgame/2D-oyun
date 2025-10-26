using System.Runtime.CompilerServices;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private int toggle;
    [SerializeField] private Button _openMenu;
    [Header("Main Menu")]
    [SerializeField] private RectTransform _mainMenu;
    [SerializeField] private Button _mainMenuBTN;
    [SerializeField] private Button _killEffectsBTN;
    [Header("KillEffects Menu")]
    [SerializeField] private RectTransform killEffectsMenu;
    [SerializeField] private Button _back_BTN;
    [SerializeField] private Button killEffect_1_BTN;
    [SerializeField] private Button killEffect_2_BTN;
    [SerializeField] private Button killEffect_3_BTN;
    [SerializeField] private Button killEffect_4_BTN;
    [Header("Health")]
    public Slider _healthSlider;
    // ---------------------------------
    private void Start()
    {
        _killEffectsBTN.onClick.AddListener(OpenKillEffectsMenu);
        _back_BTN.onClick.AddListener(CloseKillEffectsMenu);
        _openMenu.onClick.AddListener(OpenMainMenu);
        toggle = 1;

    }
    private void OpenMainMenu()
    {
        Debug.Log("Menu Acildi");
        switch (toggle)
        {
            case 0:
                _openMenu.interactable = false;
                ++toggle;
                _mainMenu.DOAnchorPosY(-1200f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    GameManager.Instance.ChangeState(GameStatesEnum.Play);
                    _openMenu.interactable = true;
                });
                break;
            case 1:
                _openMenu.interactable = false;
                GameManager.Instance.ChangeState(GameStatesEnum.Pause);
                --toggle;
                _mainMenu.DOAnchorPosY(0f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    _openMenu.interactable = true;
                });
                break;
        }
    }
    private void OpenKillEffectsMenu()
    {
        _mainMenu.DOAnchorPosY(-1200f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            _openMenu.interactable = false;
        });
        _openMenu.interactable = false;
        GameManager.Instance.ChangeState(GameStatesEnum.KillEffects);
        killEffectsMenu.DOAnchorPosY(-50f, 1f).SetEase(Ease.OutQuart);
    }
    private void CloseKillEffectsMenu()
    {
        _mainMenu.DOAnchorPosY(0f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            _openMenu.interactable = true;
        });        
        GameManager.Instance.ChangeState(GameStatesEnum.Pause);
        killEffectsMenu.DOAnchorPosY(-1200f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            _openMenu.interactable = true;
        });
    }
}
