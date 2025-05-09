using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I { get; private set; }

    public GameState CurrentState { get; private set; }

    void Start()
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
        GameState PreviousState = CurrentState;
        CurrentState = newState;

        // 状態に応じた処理
        switch (newState)
        {
            case GameState.Title:
                EventBus.StateChanged(GameState.Title);
                break;
            case GameState.Playing when PreviousState == GameState.Title:
                EventBus.StateChanged(GameState.Playing);
                break;
            case GameState.Playing when PreviousState == GameState.Pause:
                EventBus.EscapePause();
                break;
            case GameState.Result when PreviousState != GameState.Result:
                EventBus.StateChanged(GameState.Result);
                break;
            case GameState.Pause:
                EventBus.StateChanged(GameState.Pause);
                break;
            case GameState.End:
                EventBus.StateChanged(GameState.End);
                break;
        }
    }
}