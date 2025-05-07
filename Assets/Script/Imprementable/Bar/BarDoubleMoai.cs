using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class BarDoubleMoai : BarMoai
{
    private void OnEnable() => EventBus.OnDoubleMoaiLanded += OnStartBar;
    private void OnDisable() => EventBus.OnDoubleMoaiLanded -= OnStartBar;

    public override void OnStartBar(float barDuration)
    {
        base.OnStartBar(barDuration);
        isRunning = 2;
        targetValue2 = Random.Range(minValue + 20f, maxValue - 20f);
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
        if (difference < 0 && difference2 < 0)
        {
            isBar = false;
            EventBus.MoaiEyeGlow();
            await Task.Delay(1000);
            GameManager.I.SetState(GameState.Result);
        }
        float distance = (Mathf.Abs(difference) + Mathf.Abs(difference)) / 2;
        float normalizedDistance = Mathf.Clamp01(distance / 100f);
        EventBus.BarStopped(ImputEvaluater.I.Evaluate(normalizedDistance));
    }
}
