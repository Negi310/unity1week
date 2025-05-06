using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public abstract class LerpParams : ScriptableObject, ILerpParams
{
    [SerializeField] protected float duration;
    [SerializeField] protected AnimationCurve curve;

    public float Duration => duration;
    public AnimationCurve Curve => curve;

    public abstract Type ValueType { get; }

    public abstract UniTask RunLerp(Action<object> onUpdate);
}
