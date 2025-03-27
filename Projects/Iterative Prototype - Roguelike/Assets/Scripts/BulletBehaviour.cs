using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float BulletSpeed = 15f;
    [SerializeField] private float BulletDestroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;
    [SerializeField] private float BulletDamage = 1f;
    
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDestroyTime();

        SetStraightVelocity();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroysBullet & (1 << collision.gameObject.layer)) > 0)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(BulletDamage);
            }
            Destroy(gameObject);
        }
    }

    private void SetStraightVelocity()
    {
        rb.linearVelocity = transform.up * BulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, BulletDestroyTime);
    }
}