using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "LerpParams/QuaternionLerpParams")]
public class QuaternionLerpParams : ScriptableObject, ILerpParamsProvider<Quaternion>
{
    public List<LerpParams<Quaternion>> paramsList;
    public List<LerpParams<Quaternion>> GetLerpParams() => paramsList;
}
