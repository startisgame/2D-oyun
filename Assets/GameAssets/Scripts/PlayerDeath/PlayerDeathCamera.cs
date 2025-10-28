using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using System;
using TreeEditor;

public class PlayerDeathCamera : MonoBehaviour
{
    public event Action OnDeathCam;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private RectTransform _whiteScreen;
    [SerializeField] private CinemachineCamera _cinemachineCam;
    private CinemachineBasicMultiChannelPerlin perlin;
    [SerializeField] private GameManager _gmInstance;
    [SerializeField] private float _camSpeed;
    [SerializeField] private float _camRotSpeed;
    private float shakeTime;
    private bool firstDeath;
    private bool secondDeath;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _whiteScreen.localScale = Vector2.zero;
        _cinemachineCam = GetComponent<CinemachineCamera>();
        _gmInstance = GameManager.Instance;
        _gmInstance._player.GetComponent<PlayerScript>().OnTakeDamage += OnDeath;
    }
    private void OnDeath()
    {
        shakeTime = 5;
        _camRotSpeed = 0f;
        _camSpeed = 0f;
        firstDeath = true;
        Invoke(nameof(SecondReset), 1f);
        _gmInstance.ChangeState(GameStatesEnum.GameOver);
        _audioSource.PlayOneShot(_deathSound);
        _whiteScreen.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
        _whiteScreen.localScale = new Vector2(35f, 35f);
        _whiteScreen.GetComponent<Image>().DOFade(0f, 2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _whiteScreen.localScale = Vector2.zero;
            Invoke(nameof(TriggerCam), 2f);
        });
    }
    private void Update()
    {
        if (firstDeath)
        {
            _cinemachineCam.Lens.OrthographicSize = Mathf.Lerp(5f, 4f, Time.time / 1f);
        }
        if (secondDeath)
        {
            _camSpeed += Time.deltaTime;
            _camRotSpeed += Time.deltaTime;
            _cinemachineCam.Lens.OrthographicSize = Mathf.Lerp(5f, -0.04f, _camSpeed);
            _cinemachineCam.Lens.Dutch = Mathf.Lerp(0f, 270f, _camRotSpeed);
            perlin = _cinemachineCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
            shakeTime -= Time.deltaTime * 5;
            perlin.AmplitudeGain = Mathf.Lerp(5, 0f, 1 - shakeTime * 4);
            perlin.FrequencyGain = Mathf.Lerp(5, 0f, 1 - shakeTime * 4);
        }
    }
    private void SecondReset()
    {
        secondDeath = true;
    }
    private void TriggerCam()
    {
        _gmInstance._UIController._screen.localScale = Vector2.one * 3f;
        Invoke(nameof(TriggerScreen2Scale), 1f);
        firstDeath = false;
        secondDeath = false;
        _cinemachineCam.Lens.OrthographicSize = 5f;
        _cinemachineCam.Lens.Dutch = 0f;
        _audioSource.PlayOneShot(_gmInstance._startSoundd);
        perlin.AmplitudeGain = 3f;
        perlin.FrequencyGain = 0.02f;
        _gmInstance._UIController.OpenMainMenu();
        foreach (var a in _gmInstance._EnemysList)
        {
            Destroy(a.gameObject);
        }
        _gmInstance.bar.value = 100f;
        _gmInstance._EnemysList.Clear();
        OnDeathCam?.Invoke();
    }
    private void TriggerScreen2Scale()
    {
        _gmInstance._UIController.Screen2Scale();
    }
}
