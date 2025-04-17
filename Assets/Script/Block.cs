using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField, Header("スマッシャー")]
    private Smasher smasher;

    [SerializeField, Header("吹き飛ぶパワー")]
    private Vector2 smashPower;

    [SerializeField, Header("接地判定レイ長")]
    private float rayLength = 3f;

    [SerializeField, Header("地面レイヤー")]
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    
    RaycastHit2D hit;

    private bool isGrounded;

    private void Start()
    {
        try
        {
            rb = GetComponent<Rigidbody2D>();
        }
        catch
        {
            Debug.LogError("Block : Rigidbodyを取得できません");
        }
        
        smasher.AddOnSmashLisntener(HandleOnSmash);
    }

    private void FixedUpdate()
    {
        hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            rayLength,
            groundLayer
        );

        isGrounded = hit.collider != null;

        Debug.DrawRay(transform.position, Vector2.down * rayLength, isGrounded ? Color.green : Color.red);
    } 

    private void HandleOnSmash()
    {
        if (!isGrounded) return;

        rb.AddForce(smashPower, ForceMode2D.Impulse);
    }
}
