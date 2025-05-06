using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using ResultSystem;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private LerpParams[] param;

    [SerializeField] private Image transitioner;

    [SerializeField] private RectTransform titleRect;
    [SerializeField] private RectTransform barRect;
    [SerializeField] private RectTransform scoreRect;
    [SerializeField] private RectTransform resultRect;
    [SerializeField] private RectTransform barFlashRect;

    [SerializeField] private CanvasGroup titleAlpha;
    [SerializeField] private CanvasGroup startAlpha;

    [SerializeField] private Transform hammer;

    [SerializeField] private GameObject fence;
    [SerializeField] private GameObject playCanvas;
    [SerializeField] private GameObject pauseCanvas;

    private void OnEnable()
    {
        EventBus.OnStateChanged += HandleStateChange;
        EventBus.OnBarStopped += HandleBarFlash;
        EventBus.OnEscapePause += HandleEscapePause;
    }
    private void OnDisable()
    {
        EventBus.OnStateChanged -= HandleStateChange;
        EventBus.OnBarStopped -= HandleBarFlash;
        EventBus.OnEscapePause -= HandleEscapePause;
    }

    private void HandleStateChange(GameState gameState)
    {
        if (gameState == GameState.Title)
        {
            ShowTitleScreen().Forget();
        }
        else if (gameState == GameState.Playing)
        {
            StartGameplay().Forget();
        }
        else if (gameState == GameState.Result)
        {
            ShowResult().Forget();
        }
        else if (gameState == GameState.Pause)
        {
            PauseGame();
        }
        else if (gameState == GameState.End)
        {
            EndGame().Forget();
        }
    }

    private void HandleBarFlash(ImputResult result)
    {
        BarFlash().Forget();
    }

    private async UniTask ShowTitleScreen()
    {
        await param[0].RunLerp(value => transitioner.material.SetFloat("_Transition", (float)value));
        await param[2].RunLerp(value => titleRect.anchoredPosition = (Vector2)value);
        await param[3].RunLerp(value => startAlpha.alpha = (float)value);
    }

    private async UniTask StartGameplay()
    {
        await UniTask.WhenAll(
            param[4].RunLerp(value => startAlpha.alpha = (float)value),
            param[4].RunLerp(value => titleAlpha.alpha = (float)value),
            param[5].RunLerp(value => hammer.position = (Vector3)value),
            param[6].RunLerp(value => hammer.rotation = (Quaternion)value)
        );
        await UniTask.WhenAll(
            param[7].RunLerp(value => barRect.anchoredPosition = (Vector2)value),
            param[8].RunLerp(value => scoreRect.anchoredPosition = (Vector2)value)
        );
        fence.SetActive(false);
    }

    private async UniTask ShowResult()
    {
        await UniTask.WhenAll(
            param[9].RunLerp(value => barRect.anchoredPosition = (Vector2)value),
            param[10].RunLerp(value => scoreRect.anchoredPosition = (Vector2)value)
        );
        await param[11].RunLerp(value => resultRect.anchoredPosition = (Vector2)value);
    }

    private async UniTask EndGame()
    {
        await param[1].RunLerp(value => transitioner.material.SetFloat("transition", (float)value));
    }

    private async UniTask BarFlash()
    {
        await param[12].RunLerp(value => barFlashRect.anchoredPosition = (Vector2)value);
    }

    private void PauseGame()
    {
        playCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    private void HandleEscapePause()
    {
        playCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }
}
