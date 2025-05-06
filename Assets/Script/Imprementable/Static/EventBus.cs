using System;
using ResultSystem;

public static class EventBus
{
    public static event Action<float> OnBlockLanded;
    public static event Action<float> OnMoaiLanded;
    public static event Action<float> OnReceiveSmash;
    public static event Action<int> OnReceiveScore;
    public static event Action OnRequestNextTarget;
    public static event Action OnMoaiEyeGlow;
    public static event Action<ImputResult> OnBarStopped;
    public static event Action<ScoreResult> OnScoreChanged;
    public static event Action<int> OnScoreRanked;
    public static event Action<GameState> OnStateChanged;
    public static event Action OnEscapePause;

    public static void RequestNextTarget() => OnRequestNextTarget?.Invoke();
    public static void BlockLanded(float duration) => OnBlockLanded?.Invoke(duration);
    public static void MoaiLanded(float duration) => OnMoaiLanded?.Invoke(duration);
    public static void ReceiveSmash(float inputPower) => OnReceiveSmash?.Invoke(inputPower);
    public static void ReceiveScore(int score) => OnReceiveScore?.Invoke(score);
    public static void MoaiEyeGlow() => OnMoaiEyeGlow?.Invoke();
    public static void BarStopped(ImputResult result) => OnBarStopped?.Invoke(result);
    public static void ScoreChanged(ScoreResult result) => OnScoreChanged?.Invoke(result);
    public static void ScoreRanked(int rank) => OnScoreRanked?.Invoke(rank);
    public static void StateChanged(GameState gameState) => OnStateChanged?.Invoke(gameState);
    public static void EscapePause() => OnEscapePause?.Invoke();
}