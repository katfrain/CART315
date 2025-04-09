using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class Chaser : MonoBehaviour, IDamageable
{
    
    [SerializeField] private  ParticleSystem damageParticles;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private GameObject drop;
    [SerializeField] private Transform sprite;
    [SerializeField] private float speed = 2;
    
    private static float maxHealth = 30f;
    private static float baseMaxHealth = 30f;
    private static float damage = 3f;
    private static float baseDamage = 3f;
    
    private static float scaleRate = 0.2f; 
    private static int scale = 1;
    
    private Vector3 destination;
    private Player player;
    private Excavator excavator;
    private GameManager gm;
    private  Rigidbody2D rb;
    private ParticleSystem damageParticlesInstance;
    private float currentHealth;
    private ChaserSpawn spawner;
    private GameObject target;
    private float lastDamageTime;
    private float damageCooldown = 1f;
    private GameObject coinInst;
    private Room room;
    
    private bool isDead = false;
    
    void Start()
    {
        gm = GameManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        player = Player.Instance;
        
        Excavator[] excavatorsInScene = FindObjectsOfType<Excavator>();
        foreach (Excavator e in excavatorsInScene)
        {
            if (e.transform.IsChildOf(room.transform))
            {
                excavator = e;
                break;
            }
        }
        if (_healthSlider == null)
        {
            _healthSlider = GetComponentInChildren<Slider>();
        }
        if (_healthSlider != null)
        {
            _healthSlider.maxValue = currentHealth;
            _healthSlider.value = currentHealth;
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time - lastDamageTime > damageCooldown)
        {
            if (player != null)
            {
                player.Damage(damage);
                lastDamageTime = Time.time;
            }
        }

        if (other.CompareTag("Excavator") && Time.time - lastDamageTime > damageCooldown)
        {
            if (excavator != null)
            {
                excavator.Damage(damage);
                lastDamageTime = Time.time;
            }
        }
        if (other.CompareTag("Obstacle") && Time.time - lastDamageTime > damageCooldown)
        {
            other.GetComponent<Obstacle>().Damage(damage);
            lastDamageTime = Time.time;
        }
        
    }
    
    public void Initialize(ChaserSpawn s, Room r)
    {
        spawner = s;
        room = r;
    }

    void FixedUpdate()
    {
        if (excavator != null)
        {
            target = excavator.gameObject;
        }
        else
        {
            target = player.gameObject;
        }
        
        destination = target.transform.position;
        Vector2 direction = ((Vector2)destination - rb.position).normalized;
        Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
        
        if (sprite != null)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            sprite.rotation = Quaternion.Euler(0, 0, angle - 90f); // adjust if your sprite faces up
        }
    }
    
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
        SpawnDamageParticles();
        UpdateHealthSlider();
    }
    
    public void Die()
    {
        if (isDead) return;
        isDead = true;
        
        coinInst = Instantiate(drop, transform.position, Quaternion.identity);
        SpawnDamageParticles();
        Destroy(gameObject);
        Destroy(damageParticlesInstance.gameObject, 2f);
        spawner?.NotifyChaserDeath(this);
    }
    
    private void SpawnDamageParticles()
    {
        damageParticlesInstance = Instantiate(damageParticles, transform.position, Quaternion.identity);
        Destroy(damageParticlesInstance.gameObject, 2f);
    }
    
    void UpdateHealthSlider()
    {
        _healthSlider.value = currentHealth;
    }

    public static void scaleChasers()
    {
        maxHealth = baseMaxHealth + baseMaxHealth * scaleRate * scale;
        damage = baseDamage + baseDamage * scaleRate * scale;
        scale++;
    }
    public static void resetChasers()
    {
        maxHealth = baseMaxHealth;
        damage = baseDamage;
        scale = 1;
    }
    
   
}
