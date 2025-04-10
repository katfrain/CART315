using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform BulletSpawn;
    [SerializeField] private GameObject gun;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private  ParticleSystem damageParticles;
    [SerializeField] private GameObject drop;
  
    private static float scaleRate = 0.2f;
    private static int scale = 1;
    private static float maxHealth = 100f;
    private static float baseMaxHealth = 100f;
    private static float rateOfFire = 0.5f;
    private static float baseRateOfFire = 0.5f;
    
    private float _health;
    private Player _player;
    private GameObject bulletInst;
    private GameObject coinInst;
    private Vector2 worldPosition;
    private Vector2 direction;
    private float lastShotTime = 0f;
    private bool _hasLineOfSight;
    private Vector2 initialRotation;
    private ParticleSystem damageParticlesInstance;

    void Start()
    {
        _player = Player.Instance;
        _health = maxHealth;
        if (_healthSlider != null)
        {
            _healthSlider.maxValue = _health;
            _healthSlider.value = _health;
        }
        _hasLineOfSight = false;
        initialRotation = gun.transform.up;
    }
    private void Update()
    {
        if (_hasLineOfSight)
        {
            HandleGunRotation();
            HandleGunShooting();
        }
        else
        {
            resetGunRotation();
        }

    }

    private void FixedUpdate()
    {
        int layerMask = ~LayerMask.GetMask("Enemies", "Enemy Bullets", "Player Bullets", "Rooms", "Treasure");
        RaycastHit2D ray = Physics2D.Raycast(transform.position, _player.transform.position - transform.position, Mathf.Infinity, layerMask);
        if (ray.collider != null)
        {
            _hasLineOfSight = ray.collider.CompareTag("Player");
            if (_hasLineOfSight)
            {
                Debug.DrawRay(transform.position, _player.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, _player.transform.position - transform.position, Color.red);
            }
        }
    }

    private void HandleGunRotation()
    {
        Vector2 targetDirection = (_player.transform.position - transform.position).normalized;
        gun.transform.up = Vector2.Lerp(gun.transform.up, targetDirection, Time.deltaTime * 5f);
    }

    private void resetGunRotation()
    {
        gun.transform.up = Vector2.Lerp(gun.transform.up, initialRotation, Time.deltaTime * 5f);
    }

    private void HandleGunShooting()
    {
        Vector2 targetDirection = (_player.transform.position - transform.position).normalized;
        float angleDifference = Vector2.Angle(gun.transform.up, targetDirection);
        
        if (angleDifference < 5f && Time.time >= lastShotTime + rateOfFire)
        {
            bulletInst = Instantiate(Bullet, BulletSpawn.position, gun.transform.rotation);
            lastShotTime = Time.time;
        }
    }
    
    public void Damage(float damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            SpawnDamageParticles();
            coinInst = Instantiate(drop, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(damageParticlesInstance.gameObject, 2f);
        }
        SpawnDamageParticles();
        UpdateHealthSlider();
    }

    private void SpawnDamageParticles()
    {
        damageParticlesInstance = Instantiate(damageParticles, transform.position, Quaternion.identity);
        Destroy(damageParticlesInstance.gameObject, 2f);
    }

    void UpdateHealthSlider()
    {
        _healthSlider.value = _health;
    }

    public static void scaleTurrets()
    {
        maxHealth = baseMaxHealth + baseMaxHealth * scaleRate * scale;
        rateOfFire = baseRateOfFire + baseRateOfFire * scaleRate * scale;
        TurretBulletBehaviour.setDamage(TurretBulletBehaviour.getBaseBulletDamage() + TurretBulletBehaviour.getBaseBulletDamage() * scaleRate * scale);
        scale++;
    }

    public static void resetTurrets()
    {
        maxHealth = baseMaxHealth;
        rateOfFire = baseRateOfFire;
        TurretBulletBehaviour.setDamage(TurretBulletBehaviour.getBaseBulletDamage());
        scale = 1;
    }
}


