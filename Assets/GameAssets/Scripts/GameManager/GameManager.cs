using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public event Action StateChanged;
    public static GameManager Instance { get; private set; }
    private GameStatesEnum _currentState;
    public Camera _mainCamera;
    public Volume _camVolume;
    public AttackScript _attackScript;
    public Transform _player;
    public int _currentKill;
    public CameraManager _cameraManager;
    public int _pointCounter;
    public ProgressBar bar;
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
  
    }
    public void PointUI()
    {

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
