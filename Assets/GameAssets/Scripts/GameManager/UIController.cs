using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using NUnit.Framework;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;

public class UIController : MonoBehaviour
{
    private int toggle;
    private bool isCanBuyThis;
    private bool _statsMenuOpened;
    public List<Button> _allButtonsList;
    [SerializeField] private AudioClip _startSound;
    [SerializeField] private Button _openMenu;
    [Header("Canvas 2")]
    public RectTransform _screen;
    [Header("Credits")]
    [SerializeField] private Button _creditsBTN;
    [SerializeField] private Button _creditsBackBTN;
    [SerializeField] private AudioClip _soundEffect_credit;
    private bool _isCreditsMenuOpen;
    [Header("Stats Menu")]
    [SerializeField] private Material _statsBackground;
    [SerializeField] private Button _statsOpen_BTN;
    [SerializeField] private Button _statsBack_BTN;
    [SerializeField] private TextMeshProUGUI _totalPoints;
    [SerializeField] private TextMeshProUGUI _totalKills;
    [SerializeField] private AudioClip _soundEffect_stats;
    [Header("Settings Menu")]
    [SerializeField] private Button _settings_BTN;
    [SerializeField] private Button _settingBack_BTN;
    [SerializeField] private AudioClip _settingOpenSound;
    [SerializeField] private AudioClip _settingCloseSound;
    [Header("Main Menu")]
    [SerializeField] private RectTransform _mainMenu;
    [SerializeField] private AudioClip _buttonSound1;
    [SerializeField] private AudioClip _buttonSound2;
    [SerializeField] private AudioClip _buttonSound3;
    [SerializeField] private AudioClip _buttonSound4;
    [Header("Sound Effects")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _accept1;
    [SerializeField] private AudioClip _accept2;
    [SerializeField] private AudioClip _accept3;
    [SerializeField] private AudioClip _accept4;
    [SerializeField] private AudioClip _accept5;
    [SerializeField] private AudioClip _accept6;
    [Space]
    [SerializeField] private AudioClip _reject1;
    [SerializeField] private AudioClip _reject2;
    [SerializeField] private AudioClip _reject3;
    [SerializeField] private AudioClip _reject4;
    private AudioClip _currentClip;
    [Header("AttackEffects")]
    [SerializeField] private Button _AttackEffectsBTN;
    [SerializeField] private GameObject _buyEffect;
    [SerializeField] private RectTransform AttackEffectsMenu;
    private bool attackMenuOpen;
    private bool isSelected;
    private int id;
    [SerializeField] private Material menuOffset;
    [SerializeField] private Button _AttackBack_BTN;
    [SerializeField] private Button AttackEffect_1_BTN;
    [SerializeField] private Button AttackEffect_2_BTN;
    [SerializeField] private Button AttackEffect_3_BTN;
    [SerializeField] private Button AttackEffect_4_BTN;
    [SerializeField] private Button AttackEffect_5_BTN;
    [Header("KillEffects Menu")]
    private bool killMenuOpen;
    [SerializeField] private RectTransform _killEffectsMenu;
    [SerializeField] private Button _killEffects_BTN;
    [SerializeField] private Button _killBack_BTN;
    [SerializeField] private Button killEffect_1_BTN;
    [SerializeField] private Button killEffect_2_BTN;
    [SerializeField] private Button killEffect_3_BTN;
    [SerializeField] private Button killEffect_4_BTN;
    [SerializeField] private Button killEffect_5_BTN;
    [Header("Buy Checkers")]
    private bool _attackEffect_2_isBuyyed;
    private bool _attackEffect_3_isBuyyed;
    private bool _attackEffect_4_isBuyyed;
    private bool _attackEffect_5_isBuyyed;
    // - - - - - KILL EFFECTS - - - - -
    private bool _killEffect_2_isBuyyed;
    private bool _killEffect_3_isBuyyed;
    private bool _killEffect_4_isBuyyed;
    private bool _killEffect_5_isBuyyed;
    [Header("Health")]
    public Slider _healthSlider;
    // ---------------------------------
    private void Start()
    {
        GameManager.Instance._kills = _totalKills;
        GameManager.Instance._points = _totalPoints;

        _allButtonsList = new List<Button>();

        GameManager.Instance.bar = _healthSlider;
        GameManager.Instance._UIController = this;
        GameManager.Instance._startSoundd = _startSound;

        Invoke(nameof(StartSound), 0.2f);

        _statsBack_BTN.onClick.AddListener(CreditsAndStatsRandomSound);
        _statsOpen_BTN.onClick.AddListener(CreditsAndStatsRandomSound);
        _creditsBackBTN.onClick.AddListener(CreditsAndStatsRandomSound);
        _creditsBTN.onClick.AddListener(CreditsAndStatsRandomSound);

        _creditsBackBTN.onClick.AddListener(CreditsMenuClose);
        _creditsBTN.onClick.AddListener(CreditsMenuOpen);
        _statsBack_BTN.onClick.AddListener(StatsMenuClose);
        _statsOpen_BTN.onClick.AddListener(StatsMenuOpen);
        _killEffects_BTN.onClick.AddListener(OpenKillEffectsMenu);
        _killBack_BTN.onClick.AddListener(CloseKillEffectsMenu);
        _AttackEffectsBTN.onClick.AddListener(OpenAttackEffectsMenu);
        _AttackBack_BTN.onClick.AddListener(CloseAttackEffectsMenu);
        _openMenu.onClick.AddListener(OpenMainMenu);

        toggle = 1;

        id = Shader.PropertyToID("_MainTex");

        _settingBack_BTN.onClick.AddListener(SettingsMenuClose);
        _settings_BTN.onClick.AddListener(SettingsMenuOpen);

        AttackEffect_1_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_1_BTN); });
        AttackEffect_2_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_2_BTN); });
        AttackEffect_3_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_3_BTN); });
        AttackEffect_4_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_4_BTN); });
        AttackEffect_5_BTN.onClick.AddListener(() => { AttackEffectChooser(AttackEffect_5_BTN); });

        _allButtonsList.Add(_creditsBackBTN);
        _allButtonsList.Add(_creditsBTN);
        _allButtonsList.Add(_statsBack_BTN);
        _allButtonsList.Add(_statsOpen_BTN);
        _allButtonsList.Add(_settingBack_BTN);
        _allButtonsList.Add(_settings_BTN);
        _allButtonsList.Add(_AttackEffectsBTN);
        _allButtonsList.Add(_killEffects_BTN);
        _allButtonsList.Add(_killBack_BTN);
        _allButtonsList.Add(_AttackBack_BTN);
        _allButtonsList.Add(_openMenu);

        killEffect_1_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_1_BTN); });
        killEffect_2_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_2_BTN); });
        killEffect_3_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_3_BTN); });
        killEffect_4_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_4_BTN); });
        killEffect_5_BTN.onClick.AddListener(() => { KillEffectChooser(killEffect_5_BTN); });

        _creditsBackBTN.onClick.AddListener(ButtonSoundEffects);
        _creditsBTN.onClick.AddListener(ButtonSoundEffects);
        _statsOpen_BTN.onClick.AddListener(ButtonSoundEffects);
        _statsBack_BTN.onClick.AddListener(ButtonSoundEffects);
        _settingBack_BTN.onClick.AddListener(ButtonSoundEffects);
        _settings_BTN.onClick.AddListener(ButtonSoundEffects);
        _AttackEffectsBTN.onClick.AddListener(ButtonSoundEffects);
        _killEffects_BTN.onClick.AddListener(ButtonSoundEffects);
        _killBack_BTN.onClick.AddListener(ButtonSoundEffects);
        _AttackBack_BTN.onClick.AddListener(ButtonSoundEffects);
        _openMenu.onClick.AddListener(ButtonSoundEffects);

        Invoke(nameof(OpenMainMenu), .3f);
        Invoke(nameof(Screen2Scale), 2.4f);
    }

    private void Update()
    {
        if (attackMenuOpen || killMenuOpen)
        {
            float yOffset = Time.time * -0.7f;
            menuOffset.SetTextureOffset(id, new Vector2(0f, yOffset));
        }
        if (_statsMenuOpened || _isCreditsMenuOpen)
        {
            float offsets = Time.time * -0.7f;
            _statsBackground.SetTextureOffset(id, new Vector2(0f, offsets));
        }
    }

    private void CreditsAndStatsRandomSound()
    {
        int _random = UnityEngine.Random.Range(0, 2);
        switch (_random)
        {
            case 0:
                _source.PlayOneShot(_soundEffect_stats);
                break;
            case 1:
                _source.PlayOneShot(_soundEffect_credit);
                break;
        }
    }

    public void Screen2Scale()
    {
        _screen.DOScale(1f,1.5f).SetEase(Ease.OutQuart);
    }

    private void StartSound()
    {
        _source.PlayOneShot(_startSound);
    }

    private void StatsMenuOpen()
    {
        _statsMenuOpened = true;
        for (int i = 0; i < _allButtonsList.Count; i++)
        {
            _allButtonsList[i].interactable = false;
        }
        GameManager.Instance._cineCam.DOMove(new Vector3(60.4f, 138.4f, -50f), 2.5f).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            for (int i = 0; i < _allButtonsList.Count; i++)
            {
                _allButtonsList[i].interactable = true;
            }
        });
    }

    private void CreditsMenuOpen()
    {
        _isCreditsMenuOpen = true;
        for (int i = 0; i < _allButtonsList.Count; i++)
        {
            _allButtonsList[i].interactable = false;
        }
        GameManager.Instance._cineCam.DOMove(new Vector3(-60.4f, 138.4f, -50f), 2.5f).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            for (int i = 0; i < _allButtonsList.Count; i++)
            {
                _allButtonsList[i].interactable = true;
            }
        });
    }
    private void CreditsMenuClose()
    {
        for (int i = 0; i < _allButtonsList.Count; i++)
        {
            _allButtonsList[i].interactable = false;
        }
        SettingsMenuOpen();
        GameManager.Instance._cineCam.DOMoveX(0f, 2.5f).SetEase(Ease.InOutQuart);
        Invoke(nameof(CreditMenuBoolReset), 1f);
    }

    private void StatMenuBoolReset()
    {
        _statsMenuOpened = false;
    }
        private void CreditMenuBoolReset()
    {
        _isCreditsMenuOpen = false;
    }

    private void StatsMenuClose()
    {
        Invoke(nameof(StatMenuBoolReset), 1f);
        for (int i = 0; i < _allButtonsList.Count; i++)
        {
            _allButtonsList[i].interactable = false;
        }
        SettingsMenuOpen();
        GameManager.Instance._cineCam.DOMoveX(0f, 2.5f).SetEase(Ease.InOutExpo);
    }

    private void SettingsMenuOpen()
    {
        _source.PlayOneShot(_settingOpenSound);
        for (int i = 0; i < _allButtonsList.Count; i++)
        {
            _allButtonsList[i].interactable = false;
        }
        GameManager.Instance._cineCam.DOMoveY(90f, 2.5f).SetEase(Ease.InOutExpo).OnComplete(() =>
        {
            for (int i = 0; i < _allButtonsList.Count; i++)
            {
                _allButtonsList[i].interactable = true;
            }
        });
        _screen.DOScale(3f, 3.5f).SetEase(Ease.OutQuart);
    }
    private void SettingsMenuClose()
    {
        _source.PlayOneShot(_settingCloseSound);
        _settingBack_BTN.interactable = false;
        GameManager.Instance._cineCam.DOMoveY(0f, 2.5f).SetEase(Ease.InOutExpo).OnComplete(() =>
        {
            for (int i = 0; i < _allButtonsList.Count; i++)
            {
                _allButtonsList[i].interactable = true;
            }
        });
        _screen.DOScale(1f, 3.5f).SetEase(Ease.OutQuart);
    }

    private void AttackEffectChooser(Button clickedBTN)
    {
        if (!isSelected)
        {
            int pointMinimum = 0;
            isSelected = true;
            Invoke(nameof(IsSelectedReset), .5f);
            int _randomAccept = UnityEngine.Random.Range(1, 7);
            int _randomReject = UnityEngine.Random.Range(1, 5);

            if (clickedBTN == AttackEffect_1_BTN)
            {
                var _effect = Instantiate(_buyEffect);
                _effect.transform.position = AttackEffect_1_BTN.transform.position;
                Destroy(_effect.gameObject, 1f);
                GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.Slashes);
                isCanBuyThis = true;
            }
            else if (clickedBTN == AttackEffect_2_BTN)
            {
                pointMinimum = 500;
                if (GameManager.Instance._pointCounter >= pointMinimum && !_attackEffect_2_isBuyyed)
                {
                    GameManager.Instance._pointCounter -= pointMinimum;
                    var _effect = Instantiate(_buyEffect);
                    _effect.transform.position = AttackEffect_2_BTN.transform.position;
                    Destroy(_effect.gameObject, 1f);
                    GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.ChargeEffect_1);
                    isCanBuyThis = true;
                    _attackEffect_2_isBuyyed = true;
                }
                else if (_attackEffect_2_isBuyyed)
                {
                    var _effect = Instantiate(_buyEffect);
                    _effect.transform.position = AttackEffect_2_BTN.transform.position;
                    Destroy(_effect.gameObject, 1f);
                    GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.ChargeEffect_1);
                    isCanBuyThis = true;
                }
                else { isCanBuyThis = false; }

            }
            else if (clickedBTN == AttackEffect_3_BTN)
            {
                pointMinimum = 30;
                if (GameManager.Instance._pointCounter >= pointMinimum && !_attackEffect_3_isBuyyed)
                {
                    GameManager.Instance._pointCounter -= pointMinimum;
                    var _effect = Instantiate(_buyEffect);
                    _effect.transform.position = AttackEffect_3_BTN.transform.position;
                    Destroy(_effect.gameObject, 1f);
                    GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.ChargeEffect_2);
                    isCanBuyThis = true;
                    _attackEffect_3_isBuyyed = true;
                }
                else if (_attackEffect_3_isBuyyed)
                {
                    var _effect = Instantiate(_buyEffect);
                    _effect.transform.position = AttackEffect_3_BTN.transform.position;
                    Destroy(_effect.gameObject, 1f);
                    GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.ChargeEffect_2);
                    isCanBuyThis = true;
                }
                else { isCanBuyThis = false; }
            }
            else if (clickedBTN == AttackEffect_4_BTN)
            {
                pointMinimum = 1500;
                if (GameManager.Instance._pointCounter >= pointMinimum && !_attackEffect_4_isBuyyed)
                {
                    GameManager.Instance._pointCounter -= pointMinimum;
                    var _effect = Instantiate(_buyEffect);
                    _effect.transform.position = AttackEffect_4_BTN.transform.position;
                    Destroy(_effect.gameObject, 1f);
                    GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.ChargeEffect_3);
                    isCanBuyThis = true;
                    _attackEffect_4_isBuyyed = true;
                }
                else if (_attackEffect_4_isBuyyed)
                {
                    var _effect = Instantiate(_buyEffect);
                    _effect.transform.position = AttackEffect_4_BTN.transform.position;
                    Destroy(_effect.gameObject, 1f);
                    GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.ChargeEffect_3);
                    isCanBuyThis = true;
                }
                else { isCanBuyThis = false; }
            }
            else if (clickedBTN == AttackEffect_5_BTN)
            {
                pointMinimum = 2000;
                if (GameManager.Instance._pointCounter >= pointMinimum && !_attackEffect_5_isBuyyed)
                {
                    GameManager.Instance._pointCounter -= pointMinimum;
                    var _effect = Instantiate(_buyEffect);
                    _effect.transform.position = AttackEffect_5_BTN.transform.position;
                    Destroy(_effect.gameObject, 1f);
                    GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.StoneSlashEffect);
                    isCanBuyThis = true;
                    _attackEffect_5_isBuyyed = true;
                }
                else if (_attackEffect_5_isBuyyed)
                {
                    var _effect = Instantiate(_buyEffect);
                    _effect.transform.position = AttackEffect_5_BTN.transform.position;
                    Destroy(_effect.gameObject, 1f);
                    GameManager.Instance.gameObject.GetComponent<AttacStateController>().ChangeAttackState(AttackState.StoneSlashEffect);
                    isCanBuyThis = true;
                }
                else { isCanBuyThis = false; }
            }
            if (isCanBuyThis)
            {
                switch (_randomAccept)
                {
                    case 1:
                        _currentClip = _accept1;
                        break;
                    case 2:
                        _currentClip = _accept2;
                        break;
                    case 3:
                        _currentClip = _accept3;
                        break;
                    case 4:
                        _currentClip = _accept4;
                        break;
                    case 5:
                        _currentClip = _accept5;
                        break;
                    case 6:
                        _currentClip = _accept6;
                        break;
                }
            }
            else if (!isCanBuyThis)
            {
                GameManager.Instance._cameraManager.ShakeScreen(5f, .3f);
                switch (_randomReject)
                {
                    case 1:
                        _currentClip = _reject1;
                        break;
                    case 2:
                        _currentClip = _reject2;
                        break;
                    case 3:
                        _currentClip = _reject3;
                        break;
                    case 4:
                        _currentClip = _reject4;
                        break;
                }
            }
            _source.PlayOneShot(_currentClip);
            GameManager.Instance._point.text = "POINT : " + GameManager.Instance._pointCounter;
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
            int pointMinimum = 0;
            int _randomAccept = UnityEngine.Random.Range(1, 7);
            int _randomReject = UnityEngine.Random.Range(1, 5);

            if (clickedBTN == killEffect_1_BTN)
            {
                var _effect = Instantiate(_buyEffect);
                Destroy(_effect.gameObject, 1f);
                _effect.transform.position = killEffect_1_BTN.transform.position;
                GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_1);
                isCanBuyThis = true;
            }
            else if (clickedBTN == killEffect_2_BTN)
            {
                pointMinimum = 500;
                if (GameManager.Instance._pointCounter >= pointMinimum && !_killEffect_2_isBuyyed)
                {
                    GameManager.Instance._pointCounter -= pointMinimum;
                    var _effect = Instantiate(_buyEffect);
                    Destroy(_effect.gameObject, 1f);
                    _effect.transform.position = killEffect_2_BTN.transform.position;
                    GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_2);
                    isCanBuyThis = true;
                    _killEffect_2_isBuyyed = true;
                }
                else if (_killEffect_2_isBuyyed)
                {
                    var _effect = Instantiate(_buyEffect);
                    Destroy(_effect.gameObject, 1f);
                    _effect.transform.position = killEffect_2_BTN.transform.position;
                    GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_2);
                    isCanBuyThis = true;
                }
                else { isCanBuyThis = false; }
            }
            else if (clickedBTN == killEffect_3_BTN)
            {
                pointMinimum = 1000;
                if (GameManager.Instance._pointCounter >= pointMinimum && !_killEffect_3_isBuyyed)
                {
                    GameManager.Instance._pointCounter -= pointMinimum;
                    var _effect = Instantiate(_buyEffect);
                    Destroy(_effect.gameObject, 1f);
                    _effect.transform.position = killEffect_3_BTN.transform.position;
                    GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_3);
                    isCanBuyThis = true;
                    _killEffect_3_isBuyyed = true;
                }
                else if (_killEffect_3_isBuyyed)
                {
                    var _effect = Instantiate(_buyEffect);
                    Destroy(_effect.gameObject, 1f);
                    _effect.transform.position = killEffect_3_BTN.transform.position;
                    GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_3);
                    isCanBuyThis = true;
                }
                else { isCanBuyThis = false; }
            }
            else if (clickedBTN == killEffect_4_BTN)
            {
                pointMinimum = 1500;
                if (GameManager.Instance._pointCounter >= pointMinimum && !_killEffect_4_isBuyyed)
                {
                    GameManager.Instance._pointCounter -= pointMinimum;
                    var _effect = Instantiate(_buyEffect);
                    Destroy(_effect.gameObject, 1f);
                    _effect.transform.position = killEffect_4_BTN.transform.position;
                    GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_4);
                    isCanBuyThis = true;
                    _killEffect_4_isBuyyed = true;
                }
                else if (_killEffect_4_isBuyyed)
                {
                    var _effect = Instantiate(_buyEffect);
                    Destroy(_effect.gameObject, 1f);
                    _effect.transform.position = killEffect_4_BTN.transform.position;
                    GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_4);
                    isCanBuyThis = true;
                }
                else { isCanBuyThis = false; }
            }
            else if (clickedBTN == killEffect_5_BTN)
            {
                pointMinimum = 30;
                if (GameManager.Instance._pointCounter >= pointMinimum && !_killEffect_5_isBuyyed)
                {
                    GameManager.Instance._pointCounter -= pointMinimum;
                    var _effect = Instantiate(_buyEffect);
                    Destroy(_effect.gameObject, 1f);
                    _effect.transform.position = killEffect_5_BTN.transform.position;
                    GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_5);
                    isCanBuyThis = true;
                    _killEffect_5_isBuyyed = true;
                }
                else if (_killEffect_5_isBuyyed)
                {
                    var _effect = Instantiate(_buyEffect);
                    Destroy(_effect.gameObject, 1f);
                    _effect.transform.position = killEffect_5_BTN.transform.position;
                    GameManager.Instance.gameObject.GetComponent<KillStateController>().ChangeKillState(KillState.Kill_5);
                    isCanBuyThis = true;
                }
                else { isCanBuyThis = false; }
            }
            if (isCanBuyThis)
            {
                switch (_randomAccept)
                {
                    case 1:
                        _currentClip = _accept1;
                        break;
                    case 2:
                        _currentClip = _accept2;
                        break;
                    case 3:
                        _currentClip = _accept3;
                        break;
                    case 4:
                        _currentClip = _accept4;
                        break;
                    case 5:
                        _currentClip = _accept5;
                        break;
                    case 6:
                        _currentClip = _accept6;
                        break;
                }
            }
            else if (!isCanBuyThis)
            {
                GameManager.Instance._cameraManager.ShakeScreen(5, .3f);
                switch (_randomReject)
                {
                    case 1:
                        _currentClip = _reject1;
                        break;
                    case 2:
                        _currentClip = _reject2;
                        break;
                    case 3:
                        _currentClip = _reject3;
                        break;
                    case 4:
                        _currentClip = _reject4;
                        break;
                }
            }
            _source.PlayOneShot(_currentClip);
            GameManager.Instance._point.text = "POINT : " + GameManager.Instance._pointCounter;
        }
    }

    public void OpenMainMenu()
    {
        switch (toggle)
        {
            case 0:
                for(int i = 0; i < _allButtonsList.Count; i++)
                {
                    _allButtonsList[i].interactable = false;
                }
                ++toggle;
                _mainMenu.DOAnchorPosY(-1700f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    GameManager.Instance.ChangeState(GameStatesEnum.Play);
                    for(int i = 0; i < _allButtonsList.Count; i++)
                    {
                        _allButtonsList[i].interactable = true;
                    }
                });
                break;
            case 1:
                for(int i = 0; i < _allButtonsList.Count; i++)
                {
                    _allButtonsList[i].interactable = false;
                }
                GameManager.Instance.ChangeState(GameStatesEnum.Pause);
                --toggle;
                _mainMenu.DOAnchorPosY(0f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    for(int i = 0; i < _allButtonsList.Count; i++)
                    {
                        _allButtonsList[i].interactable = true;
                    }
                });
                break;
        }
    }
    private void OpenKillEffectsMenu()
    {
        killMenuOpen = true;
        for(int i = 0;i < _allButtonsList.Count; i++)
        {
            _allButtonsList[i].interactable = false;
        }
        _mainMenu.DOAnchorPosY(-1700f, 1).SetEase(Ease.OutQuart);
        _killEffectsMenu.DOAnchorPosY(-50f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            _killBack_BTN.interactable = true;
        });
    }
    private void CloseKillEffectsMenu()
    {
        killMenuOpen = false;
        _killBack_BTN.interactable = false;
        _mainMenu.DOAnchorPosY(0f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            for(int i = 0;i < _allButtonsList.Count; i++)
            {
                _allButtonsList[i].interactable = true;
            }
        });
        _killEffectsMenu.DOAnchorPosY(-1700f, 1f).SetEase(Ease.OutQuart);
    }
    private void OpenAttackEffectsMenu()
    {
        attackMenuOpen = true;
        for(int i = 0;i < _allButtonsList.Count; i++)
        {
            _allButtonsList[i].interactable = false;
        }
        _mainMenu.DOAnchorPosY(-1700f, 1f).SetEase(Ease.OutQuart);
        AttackEffectsMenu.DOAnchorPosY(-50f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            _AttackBack_BTN.interactable = true;
        });
    }
    private void CloseAttackEffectsMenu()
    {
        attackMenuOpen = false;
        _AttackBack_BTN.interactable = false;
        _mainMenu.DOAnchorPosY(0f, 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            for(int i = 0;i < _allButtonsList.Count; i++)
            {
                _allButtonsList[i].interactable = true;
            }
        });
        AttackEffectsMenu.DOAnchorPosY(-1700f, 1f).SetEase(Ease.OutQuart);
    }
    private void ButtonSoundEffects()
    {
        int _random = UnityEngine.Random.Range(1, 5);
        switch (_random)
        {
            case 1:
                _currentClip = _buttonSound1;
                break;
            case 2:
                _currentClip = _buttonSound2;
                break;
            case 3:
                _currentClip = _buttonSound3;
                break;
            case 4:
                _currentClip = _buttonSound4;
                break;
        }
        _source.PlayOneShot(_currentClip);
    }
}
