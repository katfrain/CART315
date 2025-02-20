using UnityEngine;

public class TK_Player : MonoBehaviour
{

    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        movement = new Vector2(moveX, moveY).normalized * speed;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement;  // Apply movement through physics
    }
}
