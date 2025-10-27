using UnityEngine;

public class AttacStateController : MonoBehaviour
{
    [SerializeField] private AttackState _currentAttackState;
    private void Start()
    {
        _currentAttackState = AttackState.Slashes;
    }
    public AttackState GetCurrentAttackState()
    {
        return _currentAttackState;
    }
    public void ChangeAttackState(AttackState newAttackState)
    {
        if (newAttackState == _currentAttackState) { return; }
        _currentAttackState = newAttackState;
    }
}
