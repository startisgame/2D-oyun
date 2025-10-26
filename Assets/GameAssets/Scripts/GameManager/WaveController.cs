using UnityEditor;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
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
            int random = UnityEngine.Random.Range(1, 4);
            switch (random)
            {
                case 0:
                    // TOP SPAWN
                    var enemySpawn = Instantiate(_enemyPrefab);
                    enemySpawn.transform.position = new Vector2(UnityEngine.Random.Range(leftTop.position.x, 15), leftTop.position.y);
                    break;
                case 1:
                    //RIGHT SPAWN
                    var enemySpawn1 = Instantiate(_enemyPrefab);
                    enemySpawn1.transform.position = new Vector2(rightTop.position.x, UnityEngine.Random.Range(rightTop.position.y, -15));
                    break;
                case 2:
                    //BOTTOM SPAWN
                    var enemySpawn2 = Instantiate(_enemyPrefab);
                    enemySpawn2.transform.position = new Vector2(UnityEngine.Random.Range(rightBottom.position.x, -15), rightBottom.position.y);
                    break;
                case 3:
                    //LEFT SPAWN
                    var enemySpawn3 = Instantiate(_enemyPrefab);
                    enemySpawn3.transform.position = new Vector2(leftBottom.position.x, UnityEngine.Random.Range(leftBottom.position.y, 15));
                    break;
            }
    }
    private void ControlState()
    {
        if (GameManager.Instance.GetCurrentState() == GameStatesEnum.Pause || GameManager.Instance.GetCurrentState() == GameStatesEnum.MainMenu || GameManager.Instance.GetCurrentState() == GameStatesEnum.GameOver || GameManager.Instance.GetCurrentState() == GameStatesEnum.KillEffects)
        {
            CancelInvoke(nameof(WaveSpawner));
        }
        else if (GameManager.Instance.GetCurrentState() == GameStatesEnum.Play)
        {
            InvokeRepeating(nameof(WaveSpawner), 1.5f, 1f);
        }
    }
}
