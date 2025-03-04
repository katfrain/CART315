using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Player movement speed
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Ensure there's a Rigidbody2D attached
    }

    void Update()
    {
        // Movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        // Apply movement directly to the Rigidbody2D
        rb.linearVelocity = moveDirection * moveSpeed;
    }
    
}