using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using DG.Tweening.Plugins.Options;

public class PlayerDeathCamera : MonoBehaviour
{
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private RectTransform _whiteScreen;
    [SerializeField] private CinemachineCamera _cinemachineCam;
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
        shakeTime = 5;
        _cinemachineCam = GetComponent<CinemachineCamera>();
        _gmInstance = GameManager.Instance;
        _gmInstance._player.GetComponent<PlayerScript>().OnTakeDamage += OnDeath;
    }
    private void OnDeath()
    {
        firstDeath = true;
        Invoke(nameof(SecondReset), .5f);
        _gmInstance.ChangeState(GameStatesEnum.GameOver);
        _audioSource.PlayOneShot(_deathSound);
        _whiteScreen.localScale = new Vector2(35f, 35f);
        _whiteScreen.GetComponent<Image>().DOFade(0f, 2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _whiteScreen.localScale = Vector2.zero;
            _whiteScreen.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
        });
    }
    private void Update()
    {
        if (firstDeath)
        {
            _cinemachineCam.Lens.OrthographicSize = Mathf.Lerp(5f, 4f, Time.time / 2f);
        }
        if (secondDeath)
        {
            _camSpeed += Time.deltaTime;
            _camRotSpeed += Time.deltaTime;
            _cinemachineCam.Lens.OrthographicSize = Mathf.Lerp(5f, -0.04f, _camSpeed);
            _cinemachineCam.Lens.Dutch = Mathf.Lerp(0f, 270f, _camRotSpeed);
            CinemachineBasicMultiChannelPerlin perlin = _cinemachineCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
            shakeTime -= Time.deltaTime * 5;
            perlin.AmplitudeGain = Mathf.Lerp(5, 0f, 1 - shakeTime * 4);
            perlin.FrequencyGain = Mathf.Lerp(5, 0f, 1 - shakeTime * 4);
        }
    }
    private void SecondReset()
    {
        secondDeath = true;
    }
}
