using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeathCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cinemachineCam;
    [SerializeField] private GameManager _gmInstance;
    [SerializeField] private float _camSpeed;
    [SerializeField] private float _camRotSpeed;
    private float shakeTime;
    private void Start()
    {
        shakeTime = 5;
        _cinemachineCam = GetComponent<CinemachineCamera>();
        _gmInstance = GameManager.Instance;
        InvokeRepeating(nameof(CameraGoInside), 5f, 0f);
    }
    private void Update()
    {
        if (Keyboard.current.sKey.isPressed)
        {
            CameraGoInside();
        }
    }
    private void CameraGoInside()
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
