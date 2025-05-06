using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "LerpParams/Vector2LerpParams")]
public class Vector2LerpParams : ScriptableObject, ILerpParamsProvider<Vector2>
{
    public List<LerpParams<Vector2>> paramsList;
    public List<LerpParams<Vector2>> GetLerpParams() => paramsList;
}
