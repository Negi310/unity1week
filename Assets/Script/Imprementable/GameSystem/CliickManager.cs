using UnityEngine;
using UnityEngine.SceneManagement;

public class CliickManager : MonoBehaviour
{
    public void OnPlay()
    {
        if (GameManager.I.CurrentState != GameState.Title) return;
        AudioManager.I.PlaySE(SE.Name.Click);
        GameManager.I.SetState(GameState.Playing);
    }

    public void OnTitle()
    {
        if (GameManager.I.CurrentState != GameState.Result && GameManager.I.CurrentState != GameState.Pause) return;
        AudioManager.I.PlaySE(SE.Name.Click);
        AudioManager.I.StopBGM();
        GameManager.I.SetState(GameState.End);
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPlay();
            OnTitle();
        }
    }
}
