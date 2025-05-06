using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LerpParams<T>
{
    public T start;
    public T end;
    public float duration;
    public AnimationCurve curve;
}