using UnityEngine;

public class PoseManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(GameManager.I.CurrentState == GameState.Playing)
            {
                GameManager.I.SetState(GameState.Pause);
                Time.timeScale = 0;
            }
            else if(GameManager.I.CurrentState == GameState.Pause)
            {
                Time.timeScale = 1.0f; 
                GameManager.I.SetState(GameState.Playing);
            }
        }
    }
    
}