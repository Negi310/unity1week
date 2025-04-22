using UnityEngine;
using Cysharp.Threading.Tasks;

public class BarMoai : BarBase
{
    private void OnEnable() => EventBus.I.OnMoaiLanded += OnStartBar;
    private void OnDisable() => EventBus.I.OnMoaiLanded -= OnStartBar;

    private void OnStartBar()
    {
        StartBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return;
        //StopBar();
    }

    public override void StartBar()
    {
        isRunning = true;
        currentValue = minValue;
        targetValue = Random.Range(minValue, maxValue);
        BarLoopAsync().Forget();
    }

    protected override async UniTask BarLoopAsync()
    {
        while (isRunning)
        {
            currentValue += Time.deltaTime * duration;

            if (currentValue >= maxValue || currentValue - targetValue < 0)
            {
                
                
            }

            await UniTask.Yield();
        }

        float distance = Mathf.Abs(currentValue - targetValue);
        EventBus.I.BarStopped(distance);
    }
}
