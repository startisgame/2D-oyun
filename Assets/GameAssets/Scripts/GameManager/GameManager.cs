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
    public int _AllDeathsCounter = 0;
    public HealthCharge _healthUpSc;
    public TextMeshProUGUI _totalDeaths;
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
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        _currentState = GameStatesEnum.Play;
    }
    public void CounterUI()
    {
        ++_currentKill;
        _kill.text = "KILL : " + _currentKill;
    }
    public void PointUI()
    {
        _point.text = "POINT : " + _pointCounter;
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
