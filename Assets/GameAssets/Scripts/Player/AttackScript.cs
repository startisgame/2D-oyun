using System;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public event Action OnKillEnemy;
    [SerializeField] private AudioSource slashSound;
    [SerializeField] private GameObject killEffect;
    void Start()
    {
        OnKillEnemy += GameManager.Instance.CounterUI;
        GameManager.Instance._attackScript = this;
        slashSound.Play();
        Destroy(this.gameObject, 2f);
        Invoke(nameof(TurnOFFTrigger), 1f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnKillEnemy?.Invoke();
            var deathEffectEnemy = Instantiate(killEffect);
            Destroy(deathEffectEnemy.gameObject, 1f);
            deathEffectEnemy.transform.position = other.transform.position;
            Destroy(other.gameObject);
            GameManager.Instance._cameraManager.ShakeScreen(5f,1f);
        }
    }
    private void TurnOFFTrigger()
    {
        var boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = false;
    }
}
