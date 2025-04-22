using UnityEngine;
using Cysharp.Threading.Tasks;

public class BarBlock : BarBase
{
    private int direction = 1;
    private int bounceCount = 0;

    private void OnEnable() => EventBus.I.OnBlockLanded += OnStartBar;
    private void OnDisable() => EventBus.I.OnBlockLanded -= OnStartBar;

    private void OnStartBar()
    {
        StartBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return;
        //StopBar();
    }

    public override void StartBar()
    {
        isRunning = true;
        currentValue = minValue;
        targetValue = Random.Range(minValue, maxValue);
        bounceCount = 0;
        BarLoopAsync().Forget();
    }

    protected override async UniTask BarLoopAsync()
    {
        while (isRunning)
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

        float distance = Mathf.Abs(currentValue - targetValue) + bounceCount * 0.1f;
        EventBus.I.BarStopped(distance);
    }
}