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
    private Label _label;
    private Label _pointLabel;
    private VisualElement _rootElement;
    public CameraManager _cameraManager;
    public int _pointCounter;
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
        _label.style.color = new Color(255, 255, 255, 0F);
        _pointLabel = _rootElement.Q<Label>("point-counter");
        _pointLabel.style.color = new Color(255, 255, 255, 0F);
        _currentState = GameStatesEnum.Play;
        InvokeRepeating(nameof(StarterScreen),1.5f,0f);
    }
    private void StarterScreen()
    {
        float value = Time.time * 0.01f;
        if (value < 1.1f)
        {
            _pointLabel.style.color = new Color(255, 255, 255, value);
            _label.style.color = new Color(255, 255, 255, value);
        } else if(value > 1f)
        {
            Debug.Log("oldu");
            _pointLabel.style.color = new Color(255, 255, 255, 1f);
            _label.style.color = new Color(255, 255, 255, 1f);
            CancelInvoke();
        }
    }
    public void CounterUI()
    {
        ++_currentKill;
        _label.text = "COUNTER : " + _currentKill;
    }
    public void PointUI()
    {
        _pointLabel.text = "POINTS : " + _pointCounter;
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
