using UnityEngine;
using Cysharp.Threading.Tasks;

public class BarMoai : BarBase
{
    private void OnEnable() => EventBus.OnMoaiLanded += OnStartBar;
    private void OnDisable() => EventBus.OnMoaiLanded -= OnStartBar;

    private void OnStartBar()
    {
        StartBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return;
        StopBar();
    }

    public override void StopBar()
    {
        base.StopBar();
        EventBus.MoaiEyeGlow();
    }

    protected override async UniTask BarLoopAsync()
    {
        while (isRunning)
        {
            currentValue += Time.deltaTime * duration;

            if (currentValue == targetValue)
            {

            }

            if (currentValue >= maxValue || currentValue - targetValue < 0)
            {
                
                
            }

            await UniTask.Yield();
        }

        float distance = Mathf.Abs(currentValue - targetValue);
        EventBus.BarStopped(distance);
    }
}
