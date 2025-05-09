using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class BarDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform barFill;      // 現在値の表示バー
    [SerializeField] private RectTransform targetMarker; // ターゲット位置のマーカー
    [SerializeField] private RectTransform targetMarker2;
    [SerializeField] private RectTransform targetMarker3;
    [SerializeField] private GameObject moaiShutter;     // モアイ用のシャッター表示
    [SerializeField] private RectTransform barArea;      // バーの全体領域（UI上の範囲）

    private float barWidth;
    private BarBase bar;                // 対象となるバー

    private void OnEnable()
    {
        EventBus.OnBarStarted += BarSet;
        EventBus.OnMoaiLanded += ShutterClose;
        EventBus.OnDoubleMoaiLanded += ShutterClose;
        EventBus.OnTripleMoaiLanded += ShutterClose;
        EventBus.OnDoubleBlockLanded += ShutterOpen;
        EventBus.OnTripleBlockLanded += ShutterOpen;
        EventBus.OnDynaBlockLanded += ShutterOpen;
        EventBus.OnDoubleDynaBlockLanded += ShutterOpen;
        EventBus.OnBlockLanded += ShutterOpen;
    }

    private void OnDisable()
    {
        EventBus.OnBarStarted -= BarSet;
        EventBus.OnMoaiLanded -= ShutterClose;
        EventBus.OnDoubleMoaiLanded -= ShutterClose;
        EventBus.OnTripleMoaiLanded -= ShutterClose;
        EventBus.OnDoubleBlockLanded -= ShutterOpen;
        EventBus.OnTripleBlockLanded -= ShutterOpen;
        EventBus.OnDynaBlockLanded -= ShutterOpen;
        EventBus.OnDoubleDynaBlockLanded -= ShutterOpen;
        EventBus.OnBlockLanded -= ShutterOpen;
    }

    private void Start()
    {
        barWidth = barArea.rect.width;
    }

    private void FixedUpdate()
    {
        if (bar == null || bar.isRunning == 0) return;

        DisplayBar(bar.currentValue, barFill);
        DisplayBar(bar.targetValue, targetMarker);
        DisplayBar(bar.targetValue2, targetMarker2);
        DisplayBar(bar.targetValue3, targetMarker3);
    }

    private void DisplayBar(float value, RectTransform rect)
    {
        float normalizedValue = Mathf.InverseLerp(bar.minValue, bar.maxValue, value);
        Vector2 pos = rect.anchoredPosition;
        pos.x = normalizedValue * barWidth;
        rect.anchoredPosition = pos;
    }

    private void BarSet(BarBase targetBar)
    {
        bar = targetBar;
    }

    private void ShutterOpen(float barDuration)
    {
        moaiShutter.SetActive(false);
    }

    private void ShutterClose(float barDuration)
    {
        moaiShutter.SetActive(true);
    }
}