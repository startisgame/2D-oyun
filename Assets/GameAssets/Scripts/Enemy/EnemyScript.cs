using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class EnemyScript : MonoBehaviour
{
    public EnemyScriptableO _scObject;
    private GameManager _instance;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;

    private void Start()
    {
        _instance = GameManager.Instance;
        this._player = _instance._player;
        transform.right = _player.position;
        LookAtTarget((Vector2)_player.position);
        _speed = UnityEngine.Random.Range(0.2f, 0.8f);
    }
    private void Update()
    {
        if (GameManager.Instance.GetCurrentState() != GameStatesEnum.Play) { return; }
        transform.position += (_player.position - transform.position) * _speed * Time.deltaTime;
    }
    void LookAtTarget(Vector2 targetPos)
    {
        // Hedef yönü hesapla
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

        // Açıyı hesapla
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 2D'de Z ekseninde döndür (90 derece offset - sprite yukarı bakıyorsa)
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f);

        // Artık transform.up hedefi gösterir!
    }
}
