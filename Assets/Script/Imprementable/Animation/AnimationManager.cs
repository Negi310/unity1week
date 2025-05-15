using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening.Core;
using System;
using ResultSystem;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using TMPro;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private LerpParams[] param;

    [SerializeField] private ScoreManager sm;

    [SerializeField] private Image transitioner;

    [SerializeField] private RectTransform titleRect;
    [SerializeField] private RectTransform barRect;
    [SerializeField] private RectTransform scoreRect;
    [SerializeField] private RectTransform resultRect;

    [SerializeField] private CanvasGroup barFlash;
    [SerializeField] private CanvasGroup titleAlpha;
    [SerializeField] private CanvasGroup startAlpha;

    [SerializeField] private Transform hammer;

    [SerializeField] private GameObject fence;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject playCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject resultCanvas;

    [SerializeField] private AnimationCurve curve;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        EventBus.OnStateChanged += HandleStateChange;
        EventBus.OnBarPushed += HandleBarFlash;
        EventBus.OnEscapePause += HandleEscapePause;
    }
    private void OnDisable()
    {
        EventBus.OnStateChanged -= HandleStateChange;
        EventBus.OnBarPushed -= HandleBarFlash;
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

    private void HandleBarFlash()
    {
        BarFlash().Forget();
    }

    private async UniTask ShowTitleScreen()
    {
        AudioManager.I.PlayBGM(BGM.Name.Title);
        await param[0].RunLerp(value => transitioner.material.SetFloat("_Transition", (float)value));
        await param[2].RunLerp(value => titleRect.anchoredPosition = (Vector2)value);
        await param[3].RunLerp(value => startAlpha.alpha = (float)value);
    }

    private async UniTask StartGameplay()
    {
        AudioManager.I.StopBGM();
        AudioManager.I.PlayBGM(BGM.Name.Play);
        startButton.SetActive(false);
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
        AudioManager.I.PlaySE(SE.Name.Result);
        float to = (float)sm.scores;
        await UniTask.WhenAll(
            param[9].RunLerp(value => barRect.anchoredPosition = (Vector2)value),
            param[10].RunLerp(value => scoreRect.anchoredPosition = (Vector2)value)
        );
        await param[11].RunLerp(value => resultRect.anchoredPosition = (Vector2)value);
        await DOTweenHelper.LerpAsync(0f, to, 2f, curve, (value) =>
            {
                int scoreInt = (int)value;
                scoreText.text =  $"Score: {scoreInt}";
            });
    }

    private async UniTask EndGame()
    {
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
        playCanvas.SetActive(false);
        resultCanvas.SetActive(false);
        await param[1].RunLerp(value => transitioner.material.SetFloat("_Transition", (float)value));
        EventBus.LoadScene();
    }

    private async UniTask BarFlash()
    {
        await param[12].RunLerp(value => barFlash.alpha = (float)value);
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
