using UnityEngine;
using Cysharp.Threading.Tasks;

public class BarBlock : BarBase
{
    [HideInInspector] public int direction = 1;
    [HideInInspector] public int bounceCount = 0;

    private void OnEnable() => EventBus.OnBlockLanded += OnStartBar;
    private void OnDisable() => EventBus.OnBlockLanded -= OnStartBar;

    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        bounceCount = 0;
    }

    public override async UniTask BarLoopAsync()
    {
        while (isRunning > 0)
        {
            currentValue += direction * Time.deltaTime * duration;

            if (currentValue >= maxValue)
            {
                currentValue = maxValue;
                direction = -1;
                bounceCount++;
            }
            else if (currentValue <= minValue)
            {
                currentValue = minValue;
                direction = 1;
                bounceCount++;
            }
            await UniTask.Yield();
        }
        float distance = Mathf.Abs(currentValue - targetValue) + bounceCount * 1f;
        float normalizedDistance = Mathf.Clamp01(distance / 100f);
        EventBus.BarStopped(ImputEvaluater.I.Evaluate(normalizedDistance));
    }
}