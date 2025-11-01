using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
using DG.Tweening;

public class WaveController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemysList;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyPrefab_Medium;
    [SerializeField] private GameObject _enemyPrefab_Hard;
    [SerializeField] private Transform rightTop;
    [SerializeField] private Transform leftTop;
    [SerializeField] private Transform rightBottom;
    [SerializeField] private Transform leftBottom;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private float _waveCooldown;
    private float _waveTimeValue;
    private float _nextTime;
    public int _waveCounter;
    private float _spawnRate;
    public float _WaveEnemyMaxSpeed;
    public float _WaveEnemyMinSpeed;
    private bool oneTime;
    private int _MAX_SPAWN_VALUE;
    public int _MAX_SPAWN_VALUE_COUNTER;
    private bool isEarlyFinished;
    private bool isDeath;
    public float _timeScaler;
    private void Start()
    {
        GameManager.Instance.StateChanged += ControlState;
        _spawnRate = 2f;  // Ne kadar azsa okadar fazla gelir
        _waveCounter = 1;
        IncreaseTimer();
        _WaveEnemyMinSpeed = 0.1f;
        _WaveEnemyMaxSpeed = 0.2f;
        GameManager.Instance._newEnemyMaxSpeed = _WaveEnemyMaxSpeed;
        GameManager.Instance._newEnemyMinSpeed = _WaveEnemyMinSpeed;
        GameManager.Instance._player.GetComponent<PlayerScript>().OnTakeDamage += ResetAll;
        _MAX_SPAWN_VALUE = 10;
        Time.timeScale *= _timeScaler;
    }

    private void Update()
    {
        if (!isDeath)
        {
            if (_MAX_SPAWN_VALUE_COUNTER >= _MAX_SPAWN_VALUE)
            {
                isEarlyFinished = true;
                _MAX_SPAWN_VALUE_COUNTER = 0;
                _MAX_SPAWN_VALUE += 10;
                GameManager.Instance._EnemysList.Clear();
                CancelInvoke(nameof(WaveSpawner));
            }
            
            if (GameManager.Instance.GetCurrentState() == GameStatesEnum.Play && !oneTime)
            {
                oneTime = true;
                for (int i = 0; i < GameManager.Instance._UIController._allButtonsList.Count; i++)
                {
                    GameManager.Instance._UIController._allButtonsList[i].interactable = false;
                }
                Invoke(nameof(AllButtons), 2.2f);
                _waveText.text = "WAVE " + _waveCounter;
                _waveText.gameObject.GetComponent<RectTransform>().DOScale(2.7f, 1.5f).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    _waveText.gameObject.GetComponent<RectTransform>().DOScale(0f, 1f).SetEase(Ease.InOutQuad);
                });
                _waveText.gameObject.GetComponent<RectTransform>().DOMoveY(-3f, 1.5f).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    _waveText.gameObject.GetComponent<RectTransform>().DOMoveY(55f, 1f).SetEase(Ease.InOutQuad);
                });
            }
            
            if (GameManager.Instance.GetCurrentState() == GameStatesEnum.Play)
            {
                _waveTimeValue += Time.deltaTime;
                if (isEarlyFinished)
                {
                    isEarlyFinished = false;
                    IncreaseTimer();
                    CancelInvoke(nameof(WaveSpawner));
                    ++_waveCounter;
                    _waveText.text = "WAVE " + _waveCounter;
                    _WaveEnemyMinSpeed += 0.005f;
                    _WaveEnemyMaxSpeed += 0.01f;
                    GameManager.Instance._newEnemyMaxSpeed = _WaveEnemyMaxSpeed;
                    GameManager.Instance._newEnemyMinSpeed = _WaveEnemyMinSpeed;
                    _spawnRate -= 0.005f;
                    Invoke(nameof(WaveTrigger), 3f);
                    for (int i = 0; i < GameManager.Instance._UIController._allButtonsList.Count; i++)
                    {
                        GameManager.Instance._UIController._allButtonsList[i].interactable = false;
                    }
                    Invoke(nameof(AllButtons), 2.2f);
                    _waveText.text = "WAVE " + _waveCounter;
                    _waveText.gameObject.GetComponent<RectTransform>().DOScale(2.7f, 1.5f).SetEase(Ease.OutQuart).OnComplete(() =>
                    {
                        _waveText.gameObject.GetComponent<RectTransform>().DOScale(0f, 1f).SetEase(Ease.OutQuart);
                    });
                    _waveText.gameObject.GetComponent<RectTransform>().DOMoveY(0f, 1.5f).SetEase(Ease.OutQuart).OnComplete(() =>
                    {
                        _waveText.gameObject.GetComponent<RectTransform>().DOMoveY(130f, 1f).SetEase(Ease.OutQuart);
                    });
                }
            }
        }
    }

    private void WaveSpawner()
    {
        var newEnemy = _enemyPrefab;
        int randomEnemy = UnityEngine.Random.Range(0, 3);
        switch (randomEnemy)
        {
            case 0:
                newEnemy = _enemyPrefab;
                break;
            case 1:
                newEnemy = _enemyPrefab_Medium;
                break;
            case 2:
                newEnemy = _enemyPrefab_Hard;
                break;
        }
        int random = UnityEngine.Random.Range(0, 4);
        switch (random)
        {
            case 0:
                // TOP SPAWN
                var enemySpawn = Instantiate(newEnemy);
                _enemysList.Add(enemySpawn);
                enemySpawn.transform.position = new Vector3(UnityEngine.Random.Range(leftTop.position.x, 15), leftTop.position.y, GameManager.Instance._player.position.z + -1);
                break;
            case 1:
                //RIGHT SPAWN
                var enemySpawn1 = Instantiate(newEnemy);
                _enemysList.Add(enemySpawn1);
                enemySpawn1.transform.position = new Vector3(rightTop.position.x, UnityEngine.Random.Range(rightTop.position.y, -15), GameManager.Instance._player.position.z + -1);
                break;
            case 2:
                //BOTTOM SPAWN
                var enemySpawn2 = Instantiate(newEnemy);
                _enemysList.Add(enemySpawn2);
                enemySpawn2.transform.position = new Vector3(UnityEngine.Random.Range(rightBottom.position.x, -15), rightBottom.position.y, GameManager.Instance._player.position.z + -1);
                break;
            case 3:
                //LEFT SPAWN
                var enemySpawn3 = Instantiate(newEnemy);
                _enemysList.Add(enemySpawn3);
                enemySpawn3.transform.position = new Vector3(leftBottom.position.x, UnityEngine.Random.Range(leftBottom.position.y, 15), GameManager.Instance._player.position.z + -1);
                break;
        }
        GameManager.Instance._EnemysList = _enemysList;
    }
    private void ControlState()
    {
        if (GameManager.Instance.GetCurrentState() != GameStatesEnum.Play)
        {
            CancelInvoke(nameof(WaveSpawner));
        }
        else if (GameManager.Instance.GetCurrentState() == GameStatesEnum.Play)
        {
            InvokeRepeating(nameof(WaveSpawner), 1.5f, _spawnRate);
        }
    }
    private void WaveTrigger()
    {
        InvokeRepeating(nameof(WaveSpawner), 0.1f, _spawnRate);
    }
    private void IncreaseTimer()
    {
        _nextTime = _waveTimeValue + _waveCooldown;
    }
    private void ResetAll()
    {
        isDeath = true;
        _WaveEnemyMinSpeed = 0.05f;
        _WaveEnemyMaxSpeed = 0.12f;
        _spawnRate = 2f;  // Ne kadar azsa okadar fazla gelir
        _waveCounter = 1;
    }
    private void AllButtons()
    {
        for (int i = 0; i < GameManager.Instance._UIController._allButtonsList.Count; i++)
        {
            GameManager.Instance._UIController._allButtonsList[i].interactable = true;
        }
    }
}
