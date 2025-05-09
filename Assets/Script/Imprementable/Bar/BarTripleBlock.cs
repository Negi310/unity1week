using Cysharp.Threading.Tasks;
using UnityEngine;

public class BarTripleBlock : BarBlock
{
    private float dif1;
    private float dif2;
    private float dif3;
    private void OnEnable() => EventBus.OnTripleBlockLanded += OnStartBar;
    private void OnDisable() => EventBus.OnTripleBlockLanded -= OnStartBar;
    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        isRunning = 3;
        targetValue2 = Random.Range(minValue + 40f, maxValue - 20f);
        targetValue3 = Random.Range(minValue + 40f, maxValue - 20f);
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
        float distance = (Mathf.Abs(dif1) + Mathf.Abs(dif2) + Mathf.Abs(dif3)) / 3 + bounceCount * 0.1f;
        float normalizedDistance = Mathf.Clamp01(distance / 100f);
        EventBus.BarStopped(ImputEvaluater.I.Evaluate(normalizedDistance));
    }

    public override void StopBar()
    {
        base.StopBar();
        if (Mathf.Abs(currentValue - targetValue2) < Mathf.Abs(currentValue - targetValue) && isRunning == 2)
        {
            dif1 = currentValue - targetValue2;
        }
        if (Mathf.Abs(currentValue - targetValue2) > Mathf.Abs(currentValue - targetValue) && isRunning == 2)
        {
            dif1 = currentValue - targetValue;
        }
        if (Mathf.Abs(currentValue - targetValue2) < Mathf.Abs(currentValue - targetValue) && isRunning == 1)
        {
            dif2 = currentValue - targetValue2;
        }
        if (Mathf.Abs(currentValue - targetValue2) > Mathf.Abs(currentValue - targetValue) && isRunning == 1)
        {
            dif2 = currentValue - targetValue;
        }
        if (Mathf.Abs(currentValue - targetValue2) < Mathf.Abs(currentValue - targetValue) && isRunning == 0)
        {
            dif3 = currentValue - targetValue2;
        }
        if (Mathf.Abs(currentValue - targetValue2) > Mathf.Abs(currentValue - targetValue) && isRunning == 0)
        {
            dif3 = currentValue - targetValue;
        }
    }
}
