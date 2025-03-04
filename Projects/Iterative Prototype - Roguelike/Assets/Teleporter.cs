using System;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination; // The destination to teleport to
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Temporarily stop the player's movement to avoid glitches
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            
            if (rb != null)
            {
                Vector2 velocity = rb.linearVelocity;
                rb.linearVelocity = Vector2.zero; 
                collision.gameObject.transform.position = destination.position;
                rb.linearVelocity = velocity;
            }
        }
    }
}
