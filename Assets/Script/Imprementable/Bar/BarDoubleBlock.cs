using Cysharp.Threading.Tasks;
using UnityEngine;

public class BarDoubleBlock : BarBlock
{
    private float dif1;
    private float dif2;

    private void OnEnable() => EventBus.OnDoubleBlockLanded += OnStartBar;
    private void OnDisable() => EventBus.OnDoubleBlockLanded -= OnStartBar;
    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        isRunning = 2;
        targetValue2 = Random.Range(minValue + 40f, maxValue - 20f);
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
        float distance = (dif1 + dif2) / 2 + bounceCount * 0.1f;
        float normalizedDistance = Mathf.Clamp01(distance / 100f);
        EventBus.BarStopped(ImputEvaluater.I.Evaluate(normalizedDistance));
    }

    public override void StopBar()
    {
        base.StopBar();
        if (Mathf.Abs(currentValue - targetValue2) < Mathf.Abs(currentValue - targetValue) && isRunning == 1)
        {
            dif1 = Mathf.Abs(currentValue - targetValue2);
        }
        if (Mathf.Abs(currentValue - targetValue2) > Mathf.Abs(currentValue - targetValue) && isRunning == 1)
        {
            dif1 = Mathf.Abs(currentValue - targetValue);
        }
        if (Mathf.Abs(currentValue - targetValue2) < Mathf.Abs(currentValue - targetValue) && isRunning == 0)
        {
            dif2 = Mathf.Abs(currentValue - targetValue2);
        }
        if (Mathf.Abs(currentValue - targetValue2) > Mathf.Abs(currentValue - targetValue) && isRunning == 0)
        {
            dif2 = Mathf.Abs(currentValue - targetValue);
        }
    }
}
