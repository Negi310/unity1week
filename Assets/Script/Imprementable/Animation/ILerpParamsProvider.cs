using UnityEngine;
using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public interface ILerpParams
{
    Type ValueType { get; }
    float Duration { get; }
    AnimationCurve Curve { get; }

    UniTask RunLerp(Action<object> onUpdate);
}
