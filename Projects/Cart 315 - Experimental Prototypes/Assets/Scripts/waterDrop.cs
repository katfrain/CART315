using System;
using UnityEngine;
using UnityEngine.Events;

public class waterDrop : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    public LayerMask destroysDrop;
    public static UnityEvent hitPlayer1;
    public static UnityEvent hitPlayer2;
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
        rb.linearVelocity = -(transform.right * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((destroysDrop.value & (1 << other.gameObject.layer)) > 0)
        {
            if (other.CompareTag("Player1"))
            {
                hitPlayer1.Invoke();
            }
            if (other.CompareTag("Player2"))
            {
                hitPlayer2.Invoke();
            }
            Debug.Log(other.name + " was hit!");
            Destroy(gameObject);
            
        }
    }
}
