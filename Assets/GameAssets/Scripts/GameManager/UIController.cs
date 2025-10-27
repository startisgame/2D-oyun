using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
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
    [SerializeField] private Button _AttackEffectsBTN;
    [Header("Sound Effects")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _sound1;
    [SerializeField] private AudioClip _sound2;
    [SerializeField] private AudioClip _sound3;
    private AudioClip _currentClip;
    [Header("AttackEffects Menu")]
    private bool isSelected;
    [SerializeField] private GameObject _buyEffect;
    [SerializeField] private RectTransform AttackEffectsMenu;
    private bool attackMenuOpen;
    private int id;
    [SerializeField] private Material killMenuOffset;
    [SerializeField] private Button _AttackBack_BTN;
    [SerializeField] private Button AttackEffect_1_BTN;
    [SerializeField] private Button AttackEffect_2_BTN;
    [SerializeField] private Button AttackEffect_3_BTN;
    [SerializeField] private Button AttackEffect_4_BTN;
    [SerializeField] private Button AttackEffect_5_BTN;
    [Header("KillEffects Menu")]
    [SerializeField] private Button _KillBack_BTN;
    [SerializeField] private Button killEffect_1_BTN;
    [SerializeField] private Button killEffect_2_BTN;
    [SerializeField] private Button killEffect_3_BTN;
    [SerializeField] private Button killEffect_4_BTN;
    [SerializeField] private Button killEffect_5_BTN;
    [Header("Health")]
    public Slider _healthSlider;
    // ---------------------------------
    private void Start()
    {
        _AttackEffectsBTN.onClick.AddListener(OpenKillEffectsMenu);
        _AttackBack_BTN.onClick.AddListener(CloseKillEffectsMenu);
        _openMenu.onClick.AddListener(OpenMainMenu);
        toggle = 1;
        id = Shader.PropertyToID("_MainTex");
        AttackEffect_1_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_1_BTN); });
        AttackEffect_2_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_2_BTN); });
        AttackEffect_3_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_3_BTN); });
        AttackEffect_4_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_4_BTN); });
        AttackEffect_5_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_5_BTN); });
        
        killEffect_1_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_1_BTN); });
        killEffect_2_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_2_BTN); });
        killEffect_3_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_3_BTN); });
        killEffect_4_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_4_BTN); });
        killEffect_5_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_5_BTN); });
        Invoke(nameof(OpenMainMenu),.5f);
    }

    private void Update()
    {
        if (attackMenuOpen)
        {
            float yOffset = Time.time * -0.7f;
            killMenuOffset.SetTextureOffset(id,new Vector2(0f,yOffset));
        }
    }

    private void AttackEffectChooser(Button clickedBTN)
    {
        if (!isSelected)
        {
            isSelected = true;
            Invoke(nameof(IsSelectedReset),.5f);
            int _random = UnityEngine.Random.Range(1, 3);
            switch (_random)
            {
                case 1:
                    _currentClip = _sound1;
                    break;
                case 2:
                    _currentClip = _sound2;
                    break;
                case 3:
                    _currentClip = _sound3;
                    break;
            }
            var _effect = Instantiate(_buyEffect);
            Destroy(_effect.gameObject, 1f);
            _source.PlayOneShot(_currentClip);
            if (clickedBTN == AttackEffect_1_BTN)
            {
                _effect.transform.position = AttackEffect_1_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.Slashes);
            }
            else if (clickedBTN == AttackEffect_2_BTN)
            {
                _effect.transform.position = AttackEffect_2_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.ChargeEffect_1);
            }
            else if (clickedBTN == AttackEffect_3_BTN)
            {
                _effect.transform.position = AttackEffect_3_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.ChargeEffect_2);
            }
            else if (clickedBTN == AttackEffect_4_BTN)
            {
                _effect.transform.position = AttackEffect_4_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.ChargeEffect_3);
            }
            else if (clickedBTN == AttackEffect_5_BTN)
            {
                _effect.transform.position = AttackEffect_5_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.StoneSlashEffect);
            }
        }
    }

    private void IsSelectedReset()
    {
        isSelected = false;
    }

    private void KillEffectChooser(Button clickedBTN)
    {
        if (!isSelected)
        {
            isSelected = true;
            Invoke(nameof(IsSelectedReset), .5f);
            _source.PlayOneShot(_currentClip);
            int _random = UnityEngine.Random.Range(1, 3);
            switch (_random)
            {
                case 1:
                    _currentClip = _sound1;
                    break;
                case 2:
                    _currentClip = _sound2;
                    break;
                case 3:
                    _currentClip = _sound3;
                    break;
            }
            var _effect = Instantiate(_buyEffect);
            Destroy(_effect.gameObject, 1f);
            if (clickedBTN == killEffect_1_BTN)
            {
                _effect.transform.position = killEffect_1_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_1);
            }
            else if (clickedBTN == killEffect_2_BTN)
            {
                _effect.transform.position = killEffect_2_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_2);
            }
            else if (clickedBTN == killEffect_3_BTN)
            {
                _effect.transform.position = killEffect_3_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_3);
            }
            else if (clickedBTN == killEffect_4_BTN)
            {
                _effect.transform.position = killEffect_4_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_4);
            }
            else if (clickedBTN == killEffect_5_BTN)
            {
                _effect.transform.position = killEffect_5_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_5);
            }
        }
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
        attackMenuOpen = true;
        _mainMenu.DOAnchorPosY(-1200f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            _openMenu.interactable = false;
        });
        _openMenu.interactable = false;
        GameManager.Instance.ChangeState(GameStatesEnum.KillEffects);
        AttackEffectsMenu.DOAnchorPosY(-50f, 1f).SetEase(Ease.OutQuart);
    }
    private void CloseKillEffectsMenu()
    {
        attackMenuOpen = false;
        _mainMenu.DOAnchorPosY(0f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            _openMenu.interactable = true;
        });
        GameManager.Instance.ChangeState(GameStatesEnum.Pause);
        AttackEffectsMenu.DOAnchorPosY(-1200f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            _openMenu.interactable = true;
        });
    }
    private void OnDisable()
    {
        killMenuOffset.SetTextureOffset(id, new Vector2(0f,0f));
    }
}
