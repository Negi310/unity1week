using UnityEngine;

public class RbTest : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("AddForce",2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddForce()
    {
        Vector2 force = new Vector2(10000f,0f);
        rb.AddForce(force);
    }
}
