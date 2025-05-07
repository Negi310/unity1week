using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ContactOverPoint : MonoBehaviour
{
    private bool isOver = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOver) return;
        Over().Forget();
    }

    private async UniTask Over()
    {
        isOver = true;
        await Task.Delay(400);
        GameManager.I.SetState(GameState.Result);
    }
}
