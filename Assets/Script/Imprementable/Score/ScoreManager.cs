using UnityEngine;
using ResultSystem;

public class ScoreManager : MonoBehaviour
{
    public int scores = 0;

    private int comboCount;

    private string currentRank;
    
    private void OnEnable() => EventBus.OnReceiveScore += AddScore;
    private void OnDisable() => EventBus.OnReceiveScore -= AddScore;
    private void AddScore(int score)
    {
        
        scores += score;
        comboCount = score >= 100 ? comboCount++ : 0;
        EventBus.ScoreChanged(new ScoreResult(scores, score, comboCount));
        
        if (GetRank() != currentRank)
        {
            currentRank = GetRank();
            EventBus.ScoreRanked(currentRank);
        }
    }

    private string GetRank()
    {
        if (scores >= 2000) return "S";
        if (scores >= 1500) return "A";
        if (scores >= 1000) return "B";
        if (scores >= 500) return "C";
        return "D";
    }

    public int GetScore() => scores;
    public int GetCombo() => comboCount;
}
