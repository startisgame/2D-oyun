using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _audio1;
    [SerializeField] private AudioSource _audio2;
    [SerializeField] private Slider _soundSlider;

    private void Start()
    {
        GameManager.Instance._soundValue = 1f;
        _soundSlider.onValueChanged.AddListener(OnValueChanged);
    }
    private void OnValueChanged(float newValue)
    {
        _audio1.volume = newValue;
        _audio2.volume = newValue;
        GameManager.Instance._soundValue = newValue;
    }
}
