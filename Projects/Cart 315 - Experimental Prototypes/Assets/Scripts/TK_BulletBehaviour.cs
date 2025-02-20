using UnityEngine;

public class TK_BulletBehaviour : MonoBehaviour
{
    public float TKBulletSpeed = 15f;
    public float TKBulletDestroyTime = 3f;
    public LayerMask whatDestroysTKBullet;
    public float TKBulletDamage = 1f;
    
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDestroyTime();

        SetStraightVelocity();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroysTKBullet & (1 << collision.gameObject.layer)) > 0)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(TKBulletDamage);
            }
            Destroy(gameObject);
        }
    }

    private void SetStraightVelocity()
    {
        rb.linearVelocity = transform.up * TKBulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, TKBulletDestroyTime);
    }
}
