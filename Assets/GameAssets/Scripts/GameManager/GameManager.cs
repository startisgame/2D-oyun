using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using TMPro;
using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action StateChanged;
    public MusicHandler _musicHandler;
    public float _soundValue;
    public int _AllDeathsCounter = 0;
    public HealthCharge _healthUpSc;
    public List<GameObject> _EnemysList;
    public UIController _UIController;
    public AudioClip _startSoundd;
    private GameStatesEnum _currentState;
    public Transform _cineCam;
    public Camera _mainCamera;
    public Volume _camVolume;
    public AttackScript _attackScript;
    public Transform _player;
    public int _currentKill;
    public CameraManager _cameraManager;
    public int _pointCounter;
    public Slider bar;
    public TextMeshProUGUI _point;
    public TextMeshProUGUI _kill;
    [Header("Totals")]
    public TextMeshProUGUI _totalDeaths;
    public TextMeshProUGUI _kills;
    public TextMeshProUGUI _points;
    public int _killCount;
    public int _pointCount;
    public int _TEMP_ALL_KILLS;
    public int _TEMP_ALL_POINTS;
    public int _TEMP_ALL_DEATHS;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        _currentState = GameStatesEnum.MainMenu;
    }
    public void CounterUI()
    {
        _TEMP_ALL_KILLS += 1;
        _kills.text = "TOTAL KILLS : " + _TEMP_ALL_KILLS;
        _kill.text = "KILL : " + ++_currentKill;
    }
    public void PointUI()
    {
        _TEMP_ALL_POINTS += _pointCount;
        _points.text = "TOTAL POINTS : " + _TEMP_ALL_POINTS;
        _point.text = "POINT : " + _pointCounter;
        _pointCount = 0;
    }
    public void DeathUI()
    {
        ++_TEMP_ALL_DEATHS;
        _totalDeaths.text = "TOTAL DEATHS : " + _TEMP_ALL_DEATHS;
    }
    public GameStatesEnum GetCurrentState()
    {
        return _currentState;
    }
    public void ChangeState(GameStatesEnum newState)
    {
        if (newState == _currentState) { return; }
        _currentState = newState;
        StateChanged?.Invoke();
    }
}
