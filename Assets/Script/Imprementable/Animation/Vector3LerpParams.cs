using UnityEngine;
using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(menuName = "LerpParams/Vector3LerpParams")]
public class Vector3LerpParams : LerpParams
{
    public Vector3 start;
    public Vector3 end;

    public override Type ValueType => typeof(Vector3);

    public override async UniTask RunLerp(Action<object> onUpdate)
    {
        await DOTweenHelper.LerpAsync(start, end, duration, curve, v => onUpdate(v));
    }
}