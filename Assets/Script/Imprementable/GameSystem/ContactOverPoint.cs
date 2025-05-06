using UnityEngine;

public class ContactOverPoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("over");
    }
}
