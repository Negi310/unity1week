using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Title,
        Playing,
        Result,
        Pause
    }

    public static GameManager I { get; private set; }

    public GameState CurrentState { get; private set; }

    void Awake()
    {
        if(I == null)
        {
            I = this;
            SetState(GameState.Title);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;

        // 状態に応じた処理
        switch (newState)
        {
            case GameState.Title:
                ShowTitleScreen();
                break;
            case GameState.Playing:
                StartGameplay();
                break;
            case GameState.Result:
                ShowResult();
                break;
            case GameState.Pause:
                PauseGame();
                break;
        }
    }

    private void ShowTitleScreen()
    {
        // タイトル画面の表示ロジック
    }

    private void StartGameplay()
    {
        // ゲーム開始時の初期化など
    }

    private void ShowResult()
    {
        // リザルト画面表示処理
    }

    private void PauseGame()
    {
        // ポーズ処理
    }
}