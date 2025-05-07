using Cysharp.Threading.Tasks;
using UnityEngine;

public class BarDoubleBlock : BarBlock
{
    private void OnEnable() => EventBus.OnDoubleBlockLanded += OnStartBar;
    private void OnDisable() => EventBus.OnDoubleBlockLanded -= OnStartBar;
    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        isRunning = 2;
        targetValue2 = Random.Range(minValue + 20f, maxValue - 20f);
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
        float distance = (Mathf.Abs(currentValue - targetValue2) + Mathf.Abs(currentValue - targetValue)) / 2 + bounceCount * 0.1f;
        float normalizedDistance = Mathf.Clamp01(distance / 100f);
        EventBus.BarStopped(ImputEvaluater.I.Evaluate(normalizedDistance));
    }
}
