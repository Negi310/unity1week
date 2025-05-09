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
        if (score >= 650)
        {
            comboCount += 1;
        }
        else
        {
            comboCount = 0;
        }
        score += (int)(0.01f * comboCount * score);
        scores += score;
        EventBus.ScoreChanged(new ScoreResult(scores, score, comboCount));
        
        if (GetRank() != currentRank)
        {
            currentRank = GetRank();
            EventBus.ScoreRanked(currentRank);
        }
    }

    private string GetRank()
    {
        if (scores >= 1350000) return "S";
        if (scores >= 450000) return "A";
        if (scores >= 150000) return "B";
        if (scores >= 50000) return "C";
        return "D";
    }

    public int GetScore() => scores;
    public int GetCombo() => comboCount;
}
