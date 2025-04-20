using UnityEngine;

[CreateAssetMenu(fileName = "TargetData", menuName = "Scriptable Objects/TargetData")]
public class TargetData : ScriptableObject
{
    public float powerRatio;
    public float rayLength;
    public float deactiveTime;
    public Color color;
    public LayerMask groundLayer;
    public LayerMask outLayer;
}
