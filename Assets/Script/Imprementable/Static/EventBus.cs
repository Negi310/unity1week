using System;
using ResultSystem;

public static class EventBus
{
    public static event Action<float> OnBlockLanded;
    public static event Action<float> OnMoaiLanded;
    public static event Action<float> OnDynaBlockLanded;
    public static event Action<float> OnDoubleBlockLanded;
    public static event Action<float> OnDoubleDynaBlockLanded;
    public static event Action<float> OnTripleBlockLanded;
    public static event Action<float> OnDoubleMoaiLanded;
    public static event Action<float> OnTripleMoaiLanded;
    public static event Action<float> OnReceiveSmash;
    public static event Action<int> OnReceiveScore;
    public static event Action OnRequestNextTarget;
    public static event Action OnMoaiEyeGlow;
    public static event Action<BarBase> OnBarStarted;
    public static event Action<ImputResult> OnBarStopped;
    public static event Action<ScoreResult> OnScoreChanged;
    public static event Action<string> OnScoreRanked;
    public static event Action<GameState> OnStateChanged;
    public static event Action OnEscapePause;

    public static void RequestNextTarget() => OnRequestNextTarget?.Invoke();
    public static void BlockLanded(float duration) => OnBlockLanded?.Invoke(duration);
    public static void MoaiLanded(float duration) => OnMoaiLanded?.Invoke(duration);
    public static void DynaBlockLanded(float duration) => OnDynaBlockLanded?.Invoke(duration);
    public static void DoubleBlockLanded(float duration) => OnDoubleBlockLanded?.Invoke(duration);
    public static void DoubleDynaBlockLanded(float duration) => OnDoubleDynaBlockLanded?.Invoke(duration);
    public static void TripleBlockLanded(float duration) => OnTripleBlockLanded?.Invoke(duration);
    public static void DoubleMoaiLanded(float duration) => OnDoubleMoaiLanded?.Invoke(duration);
    public static void TripleMoaiLanded(float duration) => OnTripleMoaiLanded?.Invoke(duration);
    public static void ReceiveSmash(float inputPower) => OnReceiveSmash?.Invoke(inputPower);
    public static void ReceiveScore(int score) => OnReceiveScore?.Invoke(score);
    public static void MoaiEyeGlow() => OnMoaiEyeGlow?.Invoke();
    public static void BarStarted(BarBase bar) => OnBarStarted?.Invoke(bar);
    public static void BarStopped(ImputResult result) => OnBarStopped?.Invoke(result);
    public static void ScoreChanged(ScoreResult result) => OnScoreChanged?.Invoke(result);
    public static void ScoreRanked(string rank) => OnScoreRanked?.Invoke(rank);
    public static void StateChanged(GameState gameState) => OnStateChanged?.Invoke(gameState);
    public static void EscapePause() => OnEscapePause?.Invoke();
}