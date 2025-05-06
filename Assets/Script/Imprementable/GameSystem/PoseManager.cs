using UnityEngine;

public class PoseManager : MonoBehaviour
{
    public GameObject pauseUI;
    GameObject pause;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(GameManager.I.CurrentState == GameState.Playing)
            {
                pause = Instantiate(pauseUI,this.transform);
                Time.timeScale = 0;
                GameManager.I.SetState(GameState.Pause);
            }
            else if(GameManager.I.CurrentState == GameState.Pause)
            {
                if(pause != null)
                {
                    Destroy(pause);
                }
                Time.timeScale = 1.0f; 
                GameManager.I.SetState(GameState.Playing);
            }
        }
    }
    
}