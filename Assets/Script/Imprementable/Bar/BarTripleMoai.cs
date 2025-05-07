using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class BarTripleMoai : BarMoai
{
    private void OnEnable() => EventBus.OnTripleMoaiLanded += OnStartBar;
    private void OnDisable() => EventBus.OnTripleMoaiLanded -= OnStartBar;
    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        isRunning = 3;
        targetValue2 = Random.Range(minValue + 20f, maxValue - 20f);
        targetValue3 = Random.Range(minValue + 20f, maxValue - 20f);
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
        if (difference < 0 && difference2 < 0 && difference3 < 0)
        {
            isBar = false;
            EventBus.MoaiEyeGlow();
            await Task.Delay(1000);
            GameManager.I.SetState(GameState.Result);
        }
        float distance = (Mathf.Abs(difference) + Mathf.Abs(difference2) + Mathf.Abs(difference3)) / 3;
        float normalizedDistance = Mathf.Clamp01(distance / 100f);
        EventBus.BarStopped(ImputEvaluater.I.Evaluate(normalizedDistance));
    }
}
