using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
        rb.linearVelocity = -transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            Debug.Log($"{other.name} was hit by waterdrop!");
            HosePlayer player = other.GetComponent<HosePlayer>();
            if (player != null)
            {
                if (other.CompareTag("Player1"))
                    player.hitPlayer1.Invoke();
                else if (other.CompareTag("Player2"))
                    player.hitPlayer2.Invoke();
            }

            Destroy(gameObject);
        }
    }
}