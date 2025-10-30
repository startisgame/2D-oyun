using System;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public event Action PlayerDeathAfter;
    private PlayerDeathCamera _deathCam;
    private float shakeTime;
    private float totaleShakeTime;
    private float startIntensity;

    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(-35f, 0f, 0f));
        cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
        GameManager.Instance._cameraManager = this;
        Invoke(nameof(StartScreen), 1f);
        _deathCam = GetComponent<PlayerDeathCamera>();
        _deathCam.OnDeathCam += CamTopToDown;
    }
    private void CamTopToDown()
    {
        transform.rotation = Quaternion.Euler(new Vector3(-35f, 0f, 0f));
        Invoke(nameof(StartScreen), 1f);
        PlayerDeathAfter += GameManager.Instance._musicHandler.StartMusicAfterDeath;
    }
    private void StartScreen()
    {
        Invoke(nameof(SSS), 1f);
        GameManager.Instance._currentKill = 0;
        GameManager.Instance.CounterUI();
        transform.DORotate(new Vector3(0f, 0f, 0f), 3f).SetEase(Ease.OutQuart);
    }
    private void SSS()
    {
        PlayerDeathAfter?.Invoke();
    }

    public void ShakeScreen(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        shakeTime = time;
        totaleShakeTime = time;
        startIntensity = intensity;
        cinemachineBasicMultiChannelPerlin.FrequencyGain = 5;
    }

    private void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;

            cinemachineBasicMultiChannelPerlin.AmplitudeGain = Mathf.Lerp(startIntensity, 1.5f, 1 - (shakeTime / totaleShakeTime));
            cinemachineBasicMultiChannelPerlin.FrequencyGain = Mathf.Lerp(5, 0.02f, 1 - (shakeTime / totaleShakeTime));
        }
    }

}
