using UnityEngine;
using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(menuName = "LerpParams/FloatLerpParams")]
public class FloatLerpParams : LerpParams
{
    public float start;
    public float end;

    public override Type ValueType => typeof(float);

    public override async UniTask RunLerp(Action<object> onUpdate)
    {
        await DOTweenHelper.LerpAsync(start, end, duration, curve, v => onUpdate(v));
    }
}
