using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty",menuName = "EnemySC/Empty")]
public class EnemyScriptableO : ScriptableObject
{
    public int _damage;
    public int _killPoint;
    public Color _Color;
}
