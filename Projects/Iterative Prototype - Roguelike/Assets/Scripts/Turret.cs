using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform BulletSpawn;
    [SerializeField] private float RateOfFire = 0.5f;
    [SerializeField] private GameObject gun;
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider _healthSlider;
    
    private float _health;
    private Player _player;
    private GameObject bulletInst;
    private Vector2 worldPosition;
    private Vector2 direction;
    private float lastShotTime = 0f;

    void Start()
    {
        _player = Player.Instance;
        _health = maxHealth;
        if (_healthSlider != null)
        {
            _healthSlider.maxValue = _health;
            _healthSlider.value = _health;
        }
    }
    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
    }

    private void HandleGunRotation()
    {
        direction = (new Vector2(_player.transform.position.x, _player.transform.position.y) - (Vector2)transform.position).normalized;
        gun.transform.up = direction;
    }

    private void HandleGunShooting()
    {
        if (Time.time >= lastShotTime + RateOfFire)
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
            // Add particle effects
            Destroy(gameObject);
        }

        UpdateHealthSlider();
    }

    void UpdateHealthSlider()
    {
        _healthSlider.value = _health;
    }
}


