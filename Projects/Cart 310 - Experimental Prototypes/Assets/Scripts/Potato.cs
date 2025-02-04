using System.Collections;
using UnityEngine;

public class Potato : MonoBehaviour
{
    public float bounceHeight = 5f; 
    public Sprite baseSprite;
    public Sprite hitSprite;
    public float hitSpriteDuration = 0.2f;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Sqrt(2 * bounceHeight * Mathf.Abs(Physics2D.gravity.y)));
        }
        
        sr.sprite = hitSprite;
        
        StartCoroutine(ResetSpriteAfterDelay());
        
    }

    private IEnumerator ResetSpriteAfterDelay()
    {
        yield return new WaitForSeconds(hitSpriteDuration);
        sr.sprite = baseSprite;
    }
    

    void Update()
    {
        
    }
}
