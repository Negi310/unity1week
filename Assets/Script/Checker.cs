using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private float rayLength = 2f;
    public GameObject CurrentHitObject { get; private set; }
    private void Update()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, hitLayer);
        CurrentHitObject = hit.collider?.gameObject;
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.green);
    }
}