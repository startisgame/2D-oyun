using UnityEngine;

public class KillStateController : MonoBehaviour
{
    [SerializeField] private KillState _currentKillState;

    private void Start()
    {
        _currentKillState = KillState.Kill_1;
    }

    public KillState GetCurrentKillState()
    {
        return _currentKillState;
    }

    public void ChangeKillState(KillState newKillState)
    {
        if (newKillState == _currentKillState) { return; }
        _currentKillState = newKillState;
    }

}
