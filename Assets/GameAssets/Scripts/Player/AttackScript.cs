using System;
using TMPro.EditorUtilities;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public event Action OnKillEnemy;
    [SerializeField] private HealthCharge _healthCharge;
    [SerializeField] private AudioSource slashSound;
    [Header("Audios")]
    [SerializeField] private AudioClip _currentAudioClip;
    [Header("Attack Sounds")]
    [SerializeField] private AudioClip _attackSound_1;
    [SerializeField] private AudioClip _attackSound_2;
    [SerializeField] private AudioClip _attackSound_3;                                                  
    [SerializeField] private AudioClip _attackSound_4;
    [SerializeField] private AudioClip _attackSound_5;
    [Header("Kill Sounds")]
    [SerializeField] private AudioClip _killSound_1;
    [SerializeField] private AudioClip _killSound_2;
    [SerializeField] private AudioClip _killSound_3;
    [SerializeField] private AudioClip _killSound_4;
    [SerializeField] private AudioClip _killSound_5;
    [Space]
    [SerializeField] private GameObject killEffect;
    [SerializeField] private GameObject _thousandSlashes;
    [SerializeField] private GameObject _chargeAttack1;
    [SerializeField] private GameObject _chargeAttack2;
    [SerializeField] private GameObject _chargeAttack3;
    [SerializeField] private GameObject _stoneSlashAttack;
    [Header("Kill Effects")]
    [SerializeField] private GameObject _killEffect_1;
    [SerializeField] private GameObject _killEffect_2;
    [SerializeField] private GameObject _killEffect_3;
    [SerializeField] private GameObject _killEffect_4;
    [SerializeField] private GameObject _killEffect_5;
    [SerializeField] private GameObject _currentKillEffect;
    private BoxCollider2D _boxCollider;
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        slashSound.volume = GameManager.Instance._soundValue;
        GameManager.Instance._attackScript = this;
        _healthCharge = GameManager.Instance._healthUpSc;
        OnKillEnemy += _healthCharge.HealthUp;
        OnKillEnemy += GameManager.Instance.CounterUI;
        OnKillEnemy += GameManager.Instance.PointUI;
        switch (GameManager.Instance.gameObject.GetComponent<AttacStateController>().GetCurrentAttackState())
        {
            case AttackState.Slashes:
                _boxCollider.size = new Vector2(1.72f, 1.72f);
                var attackEffect_1 = Instantiate(_thousandSlashes);
                attackEffect_1.transform.position = transform.position;
                Destroy(attackEffect_1.gameObject, 2f);
                _currentAudioClip = _attackSound_1;
                break;
            case AttackState.ChargeEffect_1:
                _boxCollider.size = new Vector2(3f, 1.72f);
                var attackEffect_2 = Instantiate(_chargeAttack1);
                attackEffect_2.transform.position = transform.position;
                Destroy(attackEffect_2.gameObject, 1.5f);
                _currentAudioClip = _attackSound_2;
                break;
            case AttackState.ChargeEffect_2:
                _boxCollider.size = new Vector2(5f, 1.72f);
                var attackEffect_3 = Instantiate(_chargeAttack2);
                attackEffect_3.transform.position = transform.position;
                Destroy(attackEffect_3.gameObject, 1.2f);
                _currentAudioClip = _attackSound_3;
                break;
            case AttackState.ChargeEffect_3:
                _boxCollider.size = new Vector2(4f, 1.72f);
                var attackEffect_4 = Instantiate(_chargeAttack3);
                attackEffect_4.transform.position = transform.position;
                Destroy(attackEffect_4.gameObject, 1.2f);
                _currentAudioClip = _attackSound_4;
                break;
            case AttackState.StoneSlashEffect:
                _boxCollider.size = new Vector2(35f, 1.5f);
                var attackEffect_5 = Instantiate(_stoneSlashAttack);
                attackEffect_5.transform.position = transform.position;
                Destroy(attackEffect_5.gameObject, 1f);
                _currentAudioClip = _attackSound_5;
                break;
        }
        slashSound.PlayOneShot(_currentAudioClip);
        Invoke(nameof(TurnOFFTrigger), 1f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var sc = other.GetComponent<EnemyScript>()._scObject._killPoint;
            GameManager.Instance._pointCounter += sc;
            GameManager.Instance._pointCount = sc;
            OnKillEnemy?.Invoke();
            switch (GameManager.Instance.gameObject.GetComponent<KillStateController>().GetCurrentKillState())
            {
                case KillState.Kill_1:
                    _currentKillEffect = _killEffect_1;
                    break;
                case KillState.Kill_2:
                    _currentKillEffect = _killEffect_2;
                    break;
                case KillState.Kill_3:
                    _currentKillEffect = _killEffect_3;
                    break;
                case KillState.Kill_4:
                    _currentKillEffect = _killEffect_4;
                    break;
                case KillState.Kill_5:
                    _currentKillEffect = _killEffect_5;
                    break;
            }
            var deathEffectEnemy = Instantiate(_currentKillEffect);
            deathEffectEnemy.transform.position = other.transform.position;
            Destroy(deathEffectEnemy.gameObject, 1f);
            Destroy(other.gameObject);
            GameManager.Instance._cameraManager.ShakeScreen(5f,.5f);
        }
    }
    private void TurnOFFTrigger()
    {
        var boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = false;
    }
}
