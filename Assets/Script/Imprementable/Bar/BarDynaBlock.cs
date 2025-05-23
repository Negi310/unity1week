using UnityEngine;
using Cysharp.Threading.Tasks;

public class BarDynaBlock : BarBlock
{
    private int targetDirection = -1;

    private void OnEnable() => EventBus.OnDynaBlockLanded += OnStartBar;
    private void OnDisable() => EventBus.OnDynaBlockLanded -= OnStartBar;
    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        TargetLoopAsync().Forget();
    }

    private async UniTask TargetLoopAsync()
    {
        while (isRunning > 0)
        {
            targetValue += targetDirection * Time.deltaTime * 0.5f * duration;
            targetValue2 = targetValue;
            targetValue3 = targetValue;

            if (targetValue >= maxValue)
            {
                targetValue = maxValue;
                targetDirection = -1;
            }
            else if (targetValue <= minValue)
            {
                targetValue = minValue;
                targetDirection = 1;
            }
            await UniTask.Yield();
        }
    }
}
