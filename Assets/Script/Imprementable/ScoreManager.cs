using UnityEngine;
using ResultSystem;

public class ScoreManager : MonoBehaviour
{
    public float scores;
    
    private void OnEnable() => EventBus.OnReceiveScore += AddScore;
    private void OnDisable() => EventBus.OnReceiveScore -= AddScore;
    private void AddScore(float score)
    {
        scores += score;
    }
}
