using UnityEngine;
using Cysharp.Threading.Tasks;

public class BarMoai : BarBase
{
    private void OnEnable() => EventBus.OnMoaiLanded += OnStartBar;
    private void OnDisable() => EventBus.OnMoaiLanded -= OnStartBar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopBar();
        }
    }

    protected override async UniTask BarLoopAsync()
    {
        while (isRunning)
        {
            currentValue += Time.deltaTime * duration;

            if (currentValue == targetValue)
            {
                EventBus.MoaiEyeGlow();
                Debug.Log("MoaiEyeGlow");
            }
            if (currentValue >= maxValue)
            {
                
            }
            await UniTask.Yield();
        }
        float difference = currentValue - targetValue;
        if (difference < 0)
        {

        }
        float distance = Mathf.Abs(difference);
        float normalizedDistance = Mathf.Clamp01(distance / 100f);
        EventBus.BarStopped(ImputEvaluater.I.Evaluate(normalizedDistance));
    }
}
