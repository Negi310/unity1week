using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BarBase : MonoBehaviour
{
    public float duration;

    public float minValue = 0f;
    public float maxValue = 100f;

    public float currentValue;
    public float targetValue;
    protected bool isRunning = false;

    public virtual void OnStartBar()
    {
        isRunning = true;
        currentValue = minValue;
        targetValue = Random.Range(minValue, maxValue);
        BarLoopAsync().Forget();
    }
    public virtual void StopBar()
    {
        isRunning = false;
    }
    protected abstract UniTask BarLoopAsync();

}