using TMPro.EditorUtilities;
using UnityEngine;

public class HealthCharge : MonoBehaviour
{
    [SerializeField] private float increase_HealthValue;
    [SerializeField] private GameObject _healthUpEffect;

    private void Start()
    {
        GameManager.Instance._healthUpSc = this;
    }
    
    public void HealthUp()
    {
        if(GameManager.Instance.bar.value < 100 && GameManager.Instance.GetCurrentState() == GameStatesEnum.Play)
        {
            var effect = Instantiate(_healthUpEffect);
            Destroy(effect.gameObject, 2f);
            effect.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z + -2);
            GameManager.Instance.bar.value += increase_HealthValue;
        }
    }
}
