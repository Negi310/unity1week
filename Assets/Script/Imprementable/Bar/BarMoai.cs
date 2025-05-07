using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class BarMoai : BarBase
{
    private void OnEnable() => EventBus.OnMoaiLanded += OnStartBar;
    private void OnDisable() => EventBus.OnMoaiLanded -= OnStartBar;

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
        if (difference < 0)
        {
            isBar = false;
            EventBus.MoaiEyeGlow();
            await Task.Delay(1000);
            GameManager.I.SetState(GameState.Result);
        }
        float distance = Mathf.Abs(difference);
        float normalizedDistance = Mathf.Clamp01(distance / 100f);
        EventBus.BarStopped(ImputEvaluater.I.Evaluate(normalizedDistance));
    }
}
