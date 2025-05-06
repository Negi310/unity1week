using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "LerpParams/FloatLerpParams")]
public class FloatLerpParams : ScriptableObject, ILerpParamsProvider<float>
{
    public List<LerpParams<float>> paramsList;
    public List<LerpParams<float>> GetLerpParams() => paramsList;
}
