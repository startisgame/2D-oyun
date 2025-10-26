using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTime;
    private float totaleShakeTime;
    private float startIntensity;
    private ProgressBar Bar;

    void Start()
    {
        cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
        GameManager.Instance._cameraManager = this;
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
