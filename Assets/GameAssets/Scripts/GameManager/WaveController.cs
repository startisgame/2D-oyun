using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

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
    private void Start()
    {
        InvokeRepeating(nameof(WaveSpawner), 1.5f, 1f);
        GameManager.Instance.StateChanged += ControlState;
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
            int random = UnityEngine.Random.Range(1, 4);
        switch (random)
        {
            case 0:
                // TOP SPAWN
                var enemySpawn = Instantiate(newEnemy);
                _enemysList.Add(enemySpawn);
                enemySpawn.transform.position = new Vector3(UnityEngine.Random.Range(leftTop.position.x, 15), leftTop.position.y,GameManager.Instance._player.position.z + -2);
                break;
            case 1:
                //RIGHT SPAWN
                var enemySpawn1 = Instantiate(newEnemy);
                _enemysList.Add(enemySpawn1);
                enemySpawn1.transform.position = new Vector3(rightTop.position.x, UnityEngine.Random.Range(rightTop.position.y, -15),GameManager.Instance._player.position.z + -2);
                break;
            case 2:
                //BOTTOM SPAWN
                var enemySpawn2 = Instantiate(newEnemy);
                _enemysList.Add(enemySpawn2);
                enemySpawn2.transform.position = new Vector3(UnityEngine.Random.Range(rightBottom.position.x, -15), rightBottom.position.y,GameManager.Instance._player.position.z + -2);
                break;
            case 3:
                //LEFT SPAWN
                var enemySpawn3 = Instantiate(newEnemy);
                _enemysList.Add(enemySpawn3);
                enemySpawn3.transform.position = new Vector3(leftBottom.position.x, UnityEngine.Random.Range(leftBottom.position.y, 15),GameManager.Instance._player.position.z + -2);
                break;
        }
        GameManager.Instance._EnemysList = _enemysList;
    }
    private void ControlState()
    {
        if (GameManager.Instance.GetCurrentState() == GameStatesEnum.Pause || GameManager.Instance.GetCurrentState() == GameStatesEnum.MainMenu || GameManager.Instance.GetCurrentState() == GameStatesEnum.GameOver || GameManager.Instance.GetCurrentState() == GameStatesEnum.KillEffects || GameManager.Instance.GetCurrentState() == GameStatesEnum.Settings)
        {
            CancelInvoke(nameof(WaveSpawner));
        }
        else if (GameManager.Instance.GetCurrentState() == GameStatesEnum.Play)
        {
            InvokeRepeating(nameof(WaveSpawner), 1.5f, 1f);
        }
    }
}
