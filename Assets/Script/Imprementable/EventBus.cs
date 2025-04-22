using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus I { get; private set; }
    public static event Action OnBlockLanded;
    public static event Action OnMoaiLanded;
    public static event Action<float, float> OnReceiveSmash;
    public static event Action OnRequestNextTarget;
    public static event Action<float> OnBarStopped;       // 距離が送られる
    public static event Action OnMoaiEyeGlow;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void RequestNextTarget() => OnRequestNextTarget?.Invoke();
    public static void BlockLanded() => OnBlockLanded?.Invoke();
    public static void MoaiLanded() => OnMoaiLanded?.Invoke();
    public static void ReceiveSmash(float inputPower, float score) => OnReceiveSmash?.Invoke(inputPower, score);
    public static void BarStopped(float distance) => OnBarStopped?.Invoke(distance);
    public static void MoaiEyeGlow() => OnMoaiEyeGlow?.Invoke();
}