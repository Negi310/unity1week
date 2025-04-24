using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BarBase : MonoBehaviour
{
    public float duration = 1f;

    [SerializeField] protected float minValue = 0f;
    [SerializeField] protected float maxValue = 100f;

    protected float currentValue;
    protected float targetValue;
    protected bool isRunning = false;

    public virtual void StartBar()
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