using UnityEngine;
using ResultSystem;

public class ScoreManager : MonoBehaviour
{
    private int scores = 0;

    private int comboCount;

    private int currentRank;
    
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

    private int GetRank()
    {
        if (scores >= 2000) return 5;
        if (scores >= 1500) return 4;
        if (scores >= 1000) return 3;
        if (scores >= 500) return 2;
        return 1;
    }

    public int GetScore() => scores;
    public int GetCombo() => comboCount;
}
