using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BarBase : MonoBehaviour
{
    [HideInInspector] public float duration;

    [HideInInspector] public float minValue = 0f;
    [HideInInspector] public float maxValue = 100f;
    [HideInInspector, Min(0)] public int isRunning = 0;
    [HideInInspector] public bool isBar;
    [HideInInspector] public bool hasGrow;
    [HideInInspector] public bool hasGrow2;
    [HideInInspector] public bool hasGrow3;


    public float currentValue;
    public float targetValue;
    public float targetValue2;
    public float targetValue3;

    public virtual void OnStartBar(float barDuration)
    {
        if (GameManager.I.CurrentState != GameState.Playing) return;
        hasGrow = true;
        hasGrow2 = true;
        hasGrow3 = true;
        isBar = true;
        isRunning = 1;
        duration = barDuration;
        currentValue = minValue;
        targetValue = Random.Range(minValue + 20f, maxValue - 20f);
        targetValue2 = targetValue;
        targetValue3 = targetValue;
        BarLoopAsync().Forget();
    }
    public virtual void StopBar()
    {
        isRunning -= 1;
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.I.CurrentState == GameState.Playing)
        {
            StopBar();
        }
    }

    public abstract UniTask BarLoopAsync();
}