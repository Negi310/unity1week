using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class BarTripleMoai : BarMoai
{
    private float dif1;
    private float dif2;
    private float dif3;
    private void OnEnable() => EventBus.OnTripleMoaiLanded += OnStartBar;
    private void OnDisable() => EventBus.OnTripleMoaiLanded -= OnStartBar;
    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        isRunning = 3;
        targetValue2 = Random.Range(minValue + 40f, maxValue - 20f);
        targetValue3 = Random.Range(minValue + 40f, maxValue - 20f);
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
            if (currentValue >= targetValue3 && hasGrow3)
            {
                EventBus.MoaiEyeGlow();
                hasGrow3 = false;
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
        float difference3 = currentValue - targetValue3;
        if (dif1 < 0 && dif2 < 0 && dif3 < 0)
        {
            isBar = false;
            EventBus.MoaiEyeGlow();
            await Task.Delay(1000);
            GameManager.I.SetState(GameState.Result);
        }
        float distance = (Mathf.Abs(dif1) + Mathf.Abs(dif2) + Mathf.Abs(dif3)) / 3;
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
