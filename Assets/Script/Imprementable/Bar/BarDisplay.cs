using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class BarDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform barFill;      // 現在値の表示バー
    [SerializeField] private RectTransform targetMarker; // ターゲット位置のマーカー
    [SerializeField] private Image moaiShutterImage;     // モアイ用のシャッター表示

    [SerializeField] private BarBase bar;                // 対象となるバー
    [SerializeField] private RectTransform barArea;      // バーの全体領域（UI上の範囲）

    [SerializeField] private Slider leftSlider;
    [SerializeField] private Slider rightSlider;

    private float barWidth;

    private void OnEnable()
    {
        EventBus.OnMoaiLanded += ShutterClose;
        EventBus.OnBlockLanded += ShutterOpen;
    }

    private void OnDisable()
    {
        EventBus.OnMoaiLanded -= ShutterClose;
        EventBus.OnBlockLanded -= ShutterOpen;
    }

    private void Start()
    {
        barWidth = barArea.rect.width;
    }

    private void FixedUpdate()
    {
        if (bar == null) return;

        float normalizedCurrent = Mathf.InverseLerp(bar.minValue, bar.maxValue, bar.currentValue);
        float normalizedTarget = Mathf.InverseLerp(bar.minValue, bar.maxValue, bar.targetValue);

        Vector2 barPosition = barFill.anchoredPosition;
        barPosition.x = normalizedCurrent * barWidth;
        barFill.anchoredPosition = barPosition;

        Vector2 targetPos = targetMarker.anchoredPosition;
        targetPos.x = normalizedTarget * barWidth;
        targetMarker.anchoredPosition = targetPos;
    }

    private async void ShutterOpen()
    {
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(50f, 0f, 0.2f, Ease.InOutQuad, (value) => leftSlider.value = value),
            DOTweenHelper.LerpAsync(50f, 0f, 0.2f, Ease.InOutQuad, (value) => rightSlider.value = value)
        );
    }

    private async void ShutterClose()
    {
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(0f, 50f, 0.2f, Ease.InOutQuad, (value) => leftSlider.value = value),
            DOTweenHelper.LerpAsync(0f, 50f, 0.2f, Ease.InOutQuad, (value) => rightSlider.value = value)
        );
    }
}