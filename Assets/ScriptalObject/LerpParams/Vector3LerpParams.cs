using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "LerpParams/Vector3LerpParams")]
public class Vector3LerpParams : ScriptableObject, ILerpParamsProvider<Vector3>
{
    public List<LerpParams<Vector3>> paramsList;
    public List<LerpParams<Vector3>> GetLerpParams() => paramsList;
}
