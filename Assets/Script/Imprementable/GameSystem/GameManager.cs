using UnityEngine;

public class GameManager : MonoBehaviour
{
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
                EventBus.StateChanged(GameState.Title);
                break;
            case GameState.Playing:
                EventBus.StateChanged(GameState.Playing);
                break;
            case GameState.Result:
                EventBus.StateChanged(GameState.Result);
                break;
            case GameState.Pause:
                EventBus.StateChanged(GameState.Pause);
                break;
        }
    }
}