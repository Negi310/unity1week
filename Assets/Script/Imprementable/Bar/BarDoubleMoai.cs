using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class BarDoubleMoai : BarMoai
{
    private float dif1;
    private float dif2;
    private void OnEnable() => EventBus.OnDoubleMoaiLanded += OnStartBar;
    private void OnDisable() => EventBus.OnDoubleMoaiLanded -= OnStartBar;

    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        isRunning = 2;
        targetValue2 = Random.Range(minValue + 40f, maxValue - 20f);
    }

    public override async UniTask BarLoopAsync()
    {
        if (GameManager.I.CurrentState != GameState.Playing) return;
        while (isRunning > 0 && isBar)
        {
            currentValue += Time.deltaTime * duration;

            if (currentValue >= targetValue && hasGrow)
            {
                EventBus.MoaiEyeGlow();
                hasGrow = false;
            }
            if (currentValue >= targetValue2 && hasGrow2)
            {
                EventBus.MoaiEyeGlow();
                hasGrow2 = false;
            }
            if (currentValue >= maxValue)
            {
                isBar = false;
                EventBus.MoaiEyeGlow();
                await Task.Delay(1000);
                GameManager.I.SetState(GameState.Result);
            }
            await UniTask.Yield();
        }
        float difference = currentValue - targetValue;
        float difference2 = currentValue - targetValue2;
        if (dif1 < 0 && dif2 < 0)
        {
            isBar = false;
            EventBus.MoaiEyeGlow();
            await Task.Delay(1000);
            GameManager.I.SetState(GameState.Result);
        }
        float distance = (Mathf.Abs(dif1) + Mathf.Abs(dif2)) / 2;
        float normalizedDistance = Mathf.Clamp01(distance / 100f);
        EventBus.BarStopped(ImputEvaluater.I.Evaluate(normalizedDistance));
    }

    public override void StopBar()
    {
        base.StopBar();
        if (Mathf.Abs(currentValue - targetValue2) < Mathf.Abs(currentValue - targetValue) && isRunning == 1)
        {
            dif1 = currentValue - targetValue2;
        }
        if (Mathf.Abs(currentValue - targetValue2) > Mathf.Abs(currentValue - targetValue) && isRunning == 1)
        {
            dif1 = currentValue - targetValue;
        }
        if (Mathf.Abs(currentValue - targetValue2) < Mathf.Abs(currentValue - targetValue) && isRunning == 0)
        {
            dif2 = currentValue - targetValue2;
        }
        if (Mathf.Abs(currentValue - targetValue2) > Mathf.Abs(currentValue - targetValue) && isRunning == 0)
        {
            dif2 = currentValue - targetValue;
        }
    }
}
