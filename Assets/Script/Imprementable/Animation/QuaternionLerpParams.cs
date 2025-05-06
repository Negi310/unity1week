using UnityEngine;
using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(menuName = "LerpParams/QuaternionLerpParams")]
public class QuaternionLerpParams : LerpParams
{
    public Quaternion start;
    public Quaternion end;

    public override Type ValueType => typeof(Quaternion);

    public override async UniTask RunLerp(Action<object> onUpdate)
    {
        await DOTweenHelper.LerpAsync(start, end, duration, curve, v => onUpdate(v));
    }
}