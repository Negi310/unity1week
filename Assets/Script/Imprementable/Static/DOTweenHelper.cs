using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;

public static class DOTweenHelper //非同期処理用の汎用関数
{
    public static async UniTask LerpAsync<T>(ILerpParamsProvider<T> provider, Action<T> updateValue)
    {
        foreach (var param in provider.GetLerpParams())
        {
            if (typeof(T) == typeof(float))
            {
                await DOTween.To(() => (float)(object)param.start, 
                                 x => updateValue((T)(object)x), 
                                 (float)(object)param.end, param.duration)
                            .SetEase(param.curve)
                            .ToUniTask();
            }
            else if (typeof(T) == typeof(Vector3))
            {
                await DOTween.To(() => (Vector3)(object)param.start, 
                                 x => updateValue((T)(object)x), 
                                 (Vector3)(object)param.end, param.duration)
                            .SetEase(param.curve)
                            .ToUniTask();
            }
            else if (typeof(T) == typeof(Quaternion))
            {
                await DOTween.To(() => ((Quaternion)(object)param.start).eulerAngles, 
                                 x => updateValue((T)(object)Quaternion.Euler(x)), 
                                 ((Quaternion)(object)param.end).eulerAngles, param.duration)
                            .SetEase(param.curve)
                            .ToUniTask();
            }
            else if (typeof(T) == typeof(Vector2))
            {
                await DOTween.To(() => (Vector2)(object)param.start, 
                                 x => updateValue((T)(object)x), 
                                 (Vector2)(object)param.end, param.duration)
                            .SetEase(param.curve)
                            .ToUniTask();
            }
        }
    }
}
