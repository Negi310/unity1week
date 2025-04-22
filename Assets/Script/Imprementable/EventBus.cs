using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus I { get; private set; }
    public event Action OnBlockLanded;
    public event Action OnMoaiLanded;
    public event Action<float, float> OnReceiveSmash;
    public event Action OnRequestNextTarget;
    public event Action<float> OnBarStopped;       // 距離が送られる
    public event Action OnMoaiEyeGlow;

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

    public void RequestNextTarget() => OnRequestNextTarget?.Invoke();
    public void BlockLanded() => OnBlockLanded?.Invoke();
    public void MoaiLanded() => OnMoaiLanded?.Invoke();
    public void ReceiveSmash(float inputPower, float score) => OnReceiveSmash?.Invoke(inputPower, score);
    public void BarStopped(float distance) => OnBarStopped?.Invoke(distance);
    public void MoaiEyeGlow() => OnMoaiEyeGlow?.Invoke();
}