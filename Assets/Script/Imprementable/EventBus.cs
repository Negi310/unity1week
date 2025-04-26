using System;
using ResultSystem;

public static class EventBus
{
    public static event Action OnBlockLanded;
    public static event Action OnMoaiLanded;
    public static event Action<float> OnReceiveSmash;
    public static event Action<float> OnReceiveScore;
    public static event Action OnRequestNextTarget;
    public static event Action OnMoaiEyeGlow;
    public static event Action<ImputResult> OnBarStopped;

    public static void RequestNextTarget() => OnRequestNextTarget?.Invoke();
    public static void BlockLanded() => OnBlockLanded?.Invoke();
    public static void MoaiLanded() => OnMoaiLanded?.Invoke();
    public static void ReceiveSmash(float inputPower) => OnReceiveSmash?.Invoke(inputPower);
    public static void ReceiveScore(float score) => OnReceiveScore?.Invoke(score);
    public static void MoaiEyeGlow() => OnMoaiEyeGlow?.Invoke();
    public static void BarStopped(ImputResult result) => OnBarStopped?.Invoke(result);
}