using System;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
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
    private Label _label;
    private VisualElement _rootElement;
    public CameraManager _cameraManager;
    public ProgressBar _healthBar;
    [SerializeField] private UIDocument _uiDoc;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        _rootElement = _uiDoc.rootVisualElement;
        var visualElement = _rootElement.Q<VisualElement>("counter-screen");
        _label = visualElement.Q<Label>("counter-text");
        _healthBar = _rootElement.Q<ProgressBar>("my-progress-bar");
        _currentState = GameStatesEnum.Play;
    }
    public void CounterUI()
    {
        ++_currentKill;
        _label.text = "COUNTER : " + _currentKill;
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
