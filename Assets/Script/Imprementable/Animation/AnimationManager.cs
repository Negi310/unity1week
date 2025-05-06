using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using ResultSystem;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private LerpParams[] param;

    private void OnEnable()
    {
        EventBus.OnStateChanged += HandleStateChange;
        EventBus.OnBarStopped += HandleBarFlash;
    }
    private void OnDisable()
    {
        EventBus.OnStateChanged -= HandleStateChange;
        EventBus.OnBarStopped -= HandleBarFlash;
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
            PauseGame().Forget();
        }
    }

    private void HandleBarFlash(ImputResult result)
    {
        BarFlash().Forget();
    }

    private async UniTask ShowTitleScreen()
    {
        await UniTask.Yield();
    }

    private async UniTask StartGameplay()
    {
        await UniTask.Yield();
    }

    private async UniTask ShowResult()
    {
        await UniTask.Yield();
    }

    private async UniTask PauseGame()
    {
        await UniTask.Yield();
    }

    private async UniTask BarFlash()
    {
        await UniTask.Yield();
    }
}
