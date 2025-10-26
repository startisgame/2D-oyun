using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Camera cameraMain;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float cooldown;
    [SerializeField] private float _currentTime;
    private bool canAttack;
    private void Start()
    {
        cameraMain = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && _currentTime < Time.time && GameManager.Instance.GetCurrentState() == GameStatesEnum.Play)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = cameraMain.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if(hit.transform == CompareTag("Player")){ return; }
            var attack = Instantiate(prefab);
            _currentTime = Time.time + cooldown;
            attack.transform.position = worldPos;
        }
    }

}
