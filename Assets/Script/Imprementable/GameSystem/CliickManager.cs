using UnityEngine;
using UnityEngine.SceneManagement;

public class CliickManager : MonoBehaviour
{
    public void OnPlay()
    {
        if (GameManager.I.CurrentState != GameState.Title) return;
        GameManager.I.SetState(GameState.Playing);
    }

    public void OnTitle()
    {
        if (GameManager.I.CurrentState != GameState.Result && GameManager.I.CurrentState != GameState.Pause) return;
        SceneManager.LoadScene("GameScene");
    }
}
