using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class MusicHandler : MonoBehaviour
{
    private List<AudioClip> _audioList;
    [SerializeField] private Button _rightBTN;
    [SerializeField] private Button _leftBTN;
    [SerializeField] private Slider _musicBar;
    private AudioSource _sourcee;
    [SerializeField] private AudioClip _music1;
    [SerializeField] private AudioClip _music2;
    [SerializeField] private AudioClip _music3;
    [SerializeField] private AudioClip _music4;
    [SerializeField] private AudioClip _music5;
    [SerializeField] private AudioClip _music6;
    [SerializeField] private AudioClip _music7;
    [SerializeField] private AudioClip _music8;
    [SerializeField] private AudioClip _music9;
    [SerializeField] private AudioClip _music10;
    [SerializeField] private AudioClip _music11;
    [SerializeField] private AudioClip _music12;
    [SerializeField] private AudioClip _music13;
    [SerializeField] private AudioClip _music14;
    [SerializeField] private AudioClip _music15;
    [SerializeField] private AudioClip _music16;
    [SerializeField] private AudioClip _music17;
    [SerializeField] private AudioClip _music18;
    private DOTween _currentTween;
    private float _currentSoundVolume;
    private int _currentAudio = 0;

    private void Start()
    {
        _audioList = new List<AudioClip>();
        _sourcee = GetComponent<AudioSource>();
        _rightBTN.onClick.AddListener(() => { MusicController(_rightBTN); });
        _leftBTN.onClick.AddListener(() => { MusicController(_leftBTN); });
        _audioList.Add(_music1);
        _audioList.Add(_music2);
        _audioList.Add(_music3);
        _audioList.Add(_music4);
        _audioList.Add(_music5);
        _audioList.Add(_music6);
        _audioList.Add(_music7);
        _audioList.Add(_music8);
        _audioList.Add(_music9);
        _audioList.Add(_music10);
        _audioList.Add(_music11);
        _audioList.Add(_music12);
        _audioList.Add(_music13);
        _audioList.Add(_music14);
        _audioList.Add(_music15);
        _audioList.Add(_music16);
        _audioList.Add(_music17);
        _audioList.Add(_music18);
        _musicBar.onValueChanged.AddListener(MusicValue);
        _sourcee.volume = 0f;
        _sourcee.Stop();
        Invoke(nameof(MusicStarter), 3f);
        GameManager.Instance._player.gameObject.GetComponent<PlayerScript>().OnTakeDamage += OnPlayerDeath;
        GameManager.Instance._musicHandler = this;
        _currentSoundVolume = 1f;
    }

    private void MusicController(Button button)
    {
        GameManager.Instance._cameraManager.ShakeScreen(3f, .3f);
        if (button == _rightBTN)
        {
            ++_currentAudio;
            if (_currentAudio >= _audioList.Count)
            {
                _currentAudio = _audioList.Count - _audioList.Count;
            }
        }
        else
        {
            --_currentAudio;
            if (_currentAudio <= 0)
            {
                _currentAudio = _audioList.Count - 1;
            }
        }
        _sourcee.Stop();
        _sourcee.clip = _audioList[_currentAudio];
        _sourcee.Play();
    }

    private void MusicStarter()
    {
        _currentAudio = UnityEngine.Random.Range(0, _audioList.Count);
        _sourcee.clip = _audioList[_currentAudio];
        _sourcee.DOFade(1f, 2f).SetEase(Ease.InSine);
        _sourcee.Play();
    }

    private void MusicValue(float volume)
    {
        _currentSoundVolume = _musicBar.maxValue - volume;
        _sourcee.volume = _currentSoundVolume;
    }

    private void OnPlayerDeath()
    {
        _sourcee.DOFade(0f, 3f).SetEase(Ease.InSine).OnComplete(() =>
        {
            _sourcee.Stop();
        });
    }
    public void StartMusicAfterDeath()
    {
        _sourcee.Play();
        _sourcee.DOFade(_currentSoundVolume, 2.5f).SetEase(Ease.InSine);
    }

}
