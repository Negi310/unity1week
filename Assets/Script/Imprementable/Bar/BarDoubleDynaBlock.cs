using UnityEngine;
using Cysharp.Threading.Tasks;

public class BarDoubleDynaBlock : BarDoubleBlock
{
    private int targetDirection = -1;
    private int target2Direction = 1;

    private void OnEnable() => EventBus.OnDoubleDynaBlockLanded += OnStartBar;
    private void OnDisable() => EventBus.OnDoubleDynaBlockLanded -= OnStartBar;
    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        Target2LoopAsync().Forget();
    }

    private async UniTask Target2LoopAsync()
    {
        while (isRunning > 0)
        {
            targetValue += targetDirection * Time.deltaTime * 0.5f * duration;
            targetValue2 += target2Direction * Time.deltaTime * 0.5f * duration;
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
            if (targetValue2 >= maxValue)
            {
                targetValue2 = maxValue;
                target2Direction = -1;
            }
            else if (targetValue2 <= minValue)
            {
                targetValue2 = minValue;
                target2Direction = 1;
            }
            await UniTask.Yield();
        }
    }
}
