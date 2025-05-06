using UnityEngine;

[CreateAssetMenu(fileName = "TargetData", menuName = "Scriptable Objects/TargetData")]
public class TargetData : ScriptableObject
{
    public AnimationCurve powerCurve;
    public float barDuration;
    public float rayLength;
    public float deactiveTime;
    public LayerMask groundLayer;
    public LayerMask outLayer;
}
