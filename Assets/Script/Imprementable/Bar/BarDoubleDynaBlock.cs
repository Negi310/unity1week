using UnityEngine;
using Cysharp.Threading.Tasks;

public class BarDoubleDynaBlock : BarDoubleBlock
{
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
            targetValue += target2Direction * Time.deltaTime * 0.5f * duration;

            if (targetValue >= maxValue)
            {
                targetValue = maxValue;
                direction = -1;
            }
            else if (targetValue <= minValue)
            {
                targetValue = minValue;
                direction = 1;
            }
            await UniTask.Yield();
        }
    }
}
