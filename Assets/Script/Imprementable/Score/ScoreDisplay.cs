using UnityEngine;
using TMPro;
using ResultSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private AnimationCurve[] scoreCurve;
    [SerializeField] private Vector3[] scoreScale;
    [SerializeField] private Vector2[] scorePos;
    [SerializeField] private CanvasGroup addedScoreCanvas;
     [SerializeField] private CanvasGroup comboCanvas;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI addedScoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI rankText;

    private void OnEnable()
    {
        EventBus.OnScoreChanged += UpdateScoreDisplay;
        EventBus.OnScoreRanked += ShowRankUp;
    }

    private void OnDisable()
    {
        EventBus.OnScoreChanged -= UpdateScoreDisplay;
        EventBus.OnScoreRanked -= ShowRankUp;
    }

    private void UpdateScoreDisplay(ScoreResult result)
    {
        addedScoreText.text = $"+{result.score}";
        ComboAnimation(result).Forget();
        AddedScoreAnimation().Forget();
        ScoreAnimation(result).Forget();
    }

    private async UniTask ScoreAnimation(ScoreResult result)
    {
        float from = (float)(result.scores - result.score);
        float to = (float)result.scores;
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(from, to, 1f, scoreCurve[0], (value) =>
            {
                int scoreInt = (int)value;
                totalScoreText.text =  $"Score: {scoreInt}";
            }),
            DOTweenHelper.LerpAsync(scoreScale[0], scoreScale[1], 1f, scoreCurve[1], (value) => totalScoreText.rectTransform.localScale = value)
        );
    }

    private async UniTask ComboAnimation(ScoreResult result)
    {
        if (result.comboCount == 0) return;
        comboText.text = $"{result.comboCount}Combo!";
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(0f, 1f, 0.7f, scoreCurve[2], (value) => comboCanvas.alpha = value),
            DOTweenHelper.LerpAsync(scoreScale[0], scoreScale[1], 1f, scoreCurve[1], (value) => comboText.rectTransform.localScale = value)
        );
    }

    private async UniTask AddedScoreAnimation()
    {
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(0f, 1f, 0.7f, scoreCurve[2], (value) => addedScoreCanvas.alpha = value),
            DOTweenHelper.LerpAsync(scorePos[0], scorePos[1], 1f, scoreCurve[3], (value) => addedScoreText.rectTransform.localPosition = value)
        );
    }

    private async void ShowRankUp(string newRank)
    {
        rankText.text = $"Rank: {newRank}!";
        await DOTweenHelper.LerpAsync(scoreScale[0], scoreScale[1], 1f, scoreCurve[1], (value) => rankText.rectTransform.localScale = value);
    }
}
