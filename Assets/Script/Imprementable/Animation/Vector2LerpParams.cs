using UnityEngine;
using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(menuName = "LerpParams/Vector2LerpParams")]
public class Vector2LerpParams : LerpParams
{
    public Vector2 start;
    public Vector2 end;

    public override Type ValueType => typeof(Vector2);

    public override async UniTask RunLerp(Action<object> onUpdate)
    {
        await DOTweenHelper.LerpAsync(start, end, duration, curve, v => onUpdate(v));
    }
}