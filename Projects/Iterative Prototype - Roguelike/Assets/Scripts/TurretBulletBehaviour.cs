using UnityEngine;

public class TurretBulletBehaviour : MonoBehaviour
{
    [SerializeField] private float BulletDestroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;
    [SerializeField] private  ParticleSystem damageParticles;
    private ParticleSystem damageParticlesInstance;
    private static float BulletDamage = 10f;
    private static float baseBulletDamage = 10f;
    private static float BulletSpeed = 15f;
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
            SpawnDamageParticles();
            if (damageable != null)
            {
                damageable.Damage(BulletDamage);
            }
            Destroy(gameObject);
        }
    }
    
    private void SpawnDamageParticles()
    {
        damageParticlesInstance = Instantiate(damageParticles, transform.position, Quaternion.identity);
        Destroy(damageParticlesInstance.gameObject, 2f);
    }

    private void SetStraightVelocity()
    {
        rb.linearVelocity = transform.up * BulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, BulletDestroyTime);
    }

    public static void setDamage(float damage)
    {
        BulletDamage = damage;
    }

    public static float getDamage()
    {
        return BulletDamage;
    }

    public static void setBulletSpeed(float speed)
    {
        BulletSpeed = speed;
    }

    public static float getBulletSpeed()
    {
        return BulletSpeed;
    }

    public static float getBaseBulletDamage()
    {
        return baseBulletDamage;
    }
}