using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BarBase : MonoBehaviour
{
    [HideInInspector] public float duration;

    [HideInInspector] public float minValue = 0f;
    [HideInInspector] public float maxValue = 100f;

    public float currentValue;
    public float targetValue;
    protected bool isRunning = false;

    public virtual void OnStartBar()
    {
        isRunning = true;
        currentValue = minValue;
        targetValue = Random.Range(minValue + 20f, maxValue - 20f);
        BarLoopAsync().Forget();
    }
    public virtual void StopBar()
    {
        isRunning = false;
    }
    protected abstract UniTask BarLoopAsync();

}