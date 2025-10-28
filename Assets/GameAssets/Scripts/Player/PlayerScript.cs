using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public event Action OnTakeDamage;
    [SerializeField] private InputActionAsset _asset;
    [SerializeField] private InputAction _isTouched;
    [SerializeField] private InputAction _touchPos;
    [SerializeField] private UIController _uiController;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float cooldown;
    [SerializeField] private float _currentTime;
    private void OnEnable()
    {
        cameraMain = Camera.main;
        _touchPos = _asset.FindActionMap("Player").FindAction("Touch");
        _isTouched = _asset.FindActionMap("Player").FindAction("Touch");
        _touchPos.Enable();
        _isTouched.Enable();
        _isTouched.performed += SpawnAttacK;
    }
    private void OnDisable()
    {
        _isTouched.performed -= SpawnAttacK;
        _touchPos.Disable();
        _isTouched.Disable();
    }

    private void Update()
    {

    }

    private void SpawnAttacK(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.GetCurrentState() == GameStatesEnum.Play && _currentTime < Time.time)
        {
            Vector2 touchPos = _touchPos.ReadValue<Vector2>();
            Vector2 worldPos = cameraMain.ScreenToWorldPoint(touchPos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.transform == CompareTag("Player")) { return; }
            else
            {
                var attack = Instantiate(prefab);
                _currentTime = Time.time + cooldown;
                attack.transform.position = worldPos;
            }
        }}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            _uiController._healthSlider.value -= 25f;
            if(_uiController._healthSlider.value <= 0)
            {
                OnTakeDamage?.Invoke();
            }
            Debug.Log(_uiController._healthSlider.value);
        }
    }

}
//Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && 