using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.OnLoadScene += Load;
    }
    private void OnDisable()
    {
        EventBus.OnLoadScene -= Load;
    }

    private void Load() => SceneManager.LoadScene("GameScene");
}
