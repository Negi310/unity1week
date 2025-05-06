using UnityEngine;
using TMPro;
using ResultSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI addedScoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI rankText;
    private Color addedScoreColor;
    private int currentTotalScore;

    private void OnEnable()
    {
        EventBus.OnScoreChanged += UpdateScoreDisplay;
        //EventBus.OnScoreRanked += ShowRankUp;
        addedScoreColor = addedScoreText.color;
    }

    private void OnDisable()
    {
        EventBus.OnScoreChanged -= UpdateScoreDisplay;
        //EventBus.OnScoreRanked -= ShowRankUp;
    }

    private void UpdateScoreDisplay(ScoreResult result)
    {
        addedScoreText.text = $"+{result.score}";
        comboText.text = $"{result.comboCount}Combo!";
        AddedScoreAnimation();
        ScoreAnimation(result);
    }

    private async void ScoreAnimation(ScoreResult result)
    {
        await UniTask.WhenAll(
            //DOTweenHelper.LerpAsync(result.scores - result.score, result.scores, 1f, Ease.InOutQuad, (value) =>
            //{
                //currentTotalScore = value;
                //totalScoreText.text =  $"+{value}";
            //}),
            //DOTweenHelper.LerpAsync(new Vector3(1f,1f,1f), new Vector3(2f,2f,2f), 1f, Ease.InOutQuad, (value) => totalScoreText.rectTransform.localScale = value),
            //DOTweenHelper.LerpAsync(new Vector3(1f,1f,1f), new Vector3(2f,2f,2f), 1f, Ease.InOutQuad, (value) => totalScoreText.rectTransform.localScale = value)
        );
        await UniTask.WhenAll(
            //DOTweenHelper.LerpAsync(new Vector3(2f,2f,2f), new Vector3(1f,1f,1f), 1f, Ease.InOutQuad, (value) => totalScoreText.rectTransform.localScale = value),
            //DOTweenHelper.LerpAsync(new Vector3(2f,2f,2f), new Vector3(1f,1f,1f), 1f, Ease.InOutQuad, (value) => totalScoreText.rectTransform.localScale = value)
        );
    }

    private async void AddedScoreAnimation()
    {
        await UniTask.WhenAll(
           // DOTweenHelper.LerpAsync(0f, 1f, 0.1f, Ease.InOutQuad, (value) => addedScoreColor.a = value),
            //DOTweenHelper.LerpAsync(new Vector2(0f,0f), new Vector2(0f,1f), 0.1f, Ease.InOutQuad, (value) => addedScoreText.rectTransform.localPosition = value)
        );
        //await DOTweenHelper.LerpAsync(1f, 0f, 0.5f, Ease.InOutQuad, (value) => addedScoreColor.a = value);
    }

    //private async void ShowRankUp(int newRank)
    //{
        //rankText.text = $"Rank {newRank}!";
        //await DOTweenHelper.LerpAsync(new Vector3(1f,1f,1f), new Vector3(2f,2f,2f), 0.5f, Ease.InOutQuad, (value) => rankText.rectTransform.localScale = value);
        //await DOTweenHelper.LerpAsync(new Vector3(2f,2f,2f), new Vector3(1f,1f,1f), 0.5f, Ease.InOutQuad, (value) => rankText.rectTransform.localScale = value);
    //}
}
