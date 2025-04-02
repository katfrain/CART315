using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Player Stats")]
    [SerializeField] private float _moveSpeed = 5f; // Player movement speed
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _damage;
    [SerializeField] private float _shootSpeed;

    [Header("Upgrade rates")]
    [SerializeField] private float _shootSpeedUpgradeRate = 1.1f;
    [SerializeField] private float _damageUpgradeRate = 1.2f;
    [SerializeField] private float _playerSpeedUpgradeRate = 1.1f;
    [SerializeField] private float _armorUpgradeRate = 1.15f;
    
    
    [Header("Player UI")]
    [SerializeField] private Slider _healthSlider;    
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _playerStats;
    
    [Header("Percentage of coins remaining upon death")]
    [SerializeField] private float _lossRate = 0.2f;
    
    [Header("Damage flash color & duration")]
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;private Rigidbody2D _rb;
    
    private Color originalColor;
    private SpriteRenderer _sr;
    private float _coinBalance;
    private float _health;

    private float _baseShootSpeed;
    private float _baseDamage;
    private float _basePlayerSpeed;
    private float _baseArmor;
    
    // Levels
    private int _shootSpeedLevel = 1;
    private const int SHOOT_SPEED_MAX_LEVEL = 9;
    private int _damageLevel = 1;
    private const int DAMAGE_MAX_LEVEL = 11;
    private int _playerSpeedLevel = 1;
    private const int PLAYER_SPEED_MAX_LEVEL = 6;
    private int _armorLevel = 1;
    private const int ARMOR_MAX_LEVEL = 11;

    public static Player Instance;

    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            updateHealthSlider();
        }
    }

    public float MaxHealth
    {
        get => _maxHealth;
    }

    private void updateStats()
    {
        string str = "Move Speed: " + _basePlayerSpeed.ToString("F1") + " + " + (_moveSpeed - _basePlayerSpeed).ToString("F1") +
                     "\nShoot Speed: " + _baseShootSpeed.ToString("F1") + " + " + (_shootSpeed - _baseShootSpeed).ToString("F1") +
                     "\nDamage: " + _baseDamage.ToString("F1") + " + " + (_damage - _baseDamage).ToString("F1") + 
                     "\nMax Health: " + _baseArmor.ToString("F1") + " + " + (_maxHealth - _baseArmor).ToString("F1");

        _playerStats.text = str;
    }
    
    public int getShootSpeedMaxLevel()
    {
        return SHOOT_SPEED_MAX_LEVEL;
    }
    public int getDamageMaxLevel()
    {
        return DAMAGE_MAX_LEVEL;
    }
    public int getPlayerSpeedMaxLevel()
    {
        return PLAYER_SPEED_MAX_LEVEL;
    }
    public int getArmorMaxLevel()
    {
        return ARMOR_MAX_LEVEL;
    }
    public bool upgradeShootSpeed(float value)
    {
        if (_shootSpeedLevel < SHOOT_SPEED_MAX_LEVEL)
        {
            if (makePurchase(value))
            {
                _shootSpeed *= _shootSpeedUpgradeRate;
                BulletBehaviour.setBulletSpeed(_shootSpeed);
                _shootSpeedLevel++;
                updateStats();
                return true;
            }
        }
        else
        {
            Debug.Log("Already Max Level!");
        }
        return false;
    }
    
    public bool upgradeDamage(float value)
    {
        Debug.Log("Player damage was: " + _damage);
        if (_damageLevel < DAMAGE_MAX_LEVEL)
        {
            if (makePurchase(value))
            {
                _damage *= _damageUpgradeRate;
                BulletBehaviour.setDamage(_damage);
                _damageLevel++;
                updateStats();
                Debug.Log("Player damage is: " + _damage);
                return true;
            }
        }
        else
        {
            Debug.Log("Already Max Level!");
        }
        return false;
    }
    
    public bool upgradePlayerSpeed(float value)
    {
        if (_playerSpeedLevel < PLAYER_SPEED_MAX_LEVEL)
        {
            if (makePurchase(value))
            {
                _moveSpeed *= _playerSpeedUpgradeRate;
                _playerSpeedLevel++;
                updateStats();
                return true;
            }
        }
        else
        {
            Debug.Log("Already Max Level!");
        }
        return false;
    }
    
    public bool upgradeArmor(float value)
    {
        if (_armorLevel < ARMOR_MAX_LEVEL)
        {
            if (makePurchase(value))
            {
                _maxHealth *= _armorUpgradeRate;
                _health = _maxHealth;
                _healthSlider.maxValue = _maxHealth;
                _armorLevel++;
                updateHealthSlider();
                RectTransform sliderRect = _healthSlider.GetComponent<RectTransform>();
                sliderRect.sizeDelta = new Vector2(sliderRect.sizeDelta.x * _armorUpgradeRate, sliderRect.sizeDelta.y);
                updateStats();
                return true;
            }
        }
        else
        {
            Debug.Log("Already Max Level!");
        }
        return false;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _health = _maxHealth;
        _rb = GetComponent<Rigidbody2D>(); 
        _sr = GetComponent<SpriteRenderer>();
        originalColor = _sr.color;
        _coinBalance = 0;
        updateCoinBalance();
        updateStats();
        if (_healthSlider != null)
        {
            _healthSlider.maxValue = _health;
            _healthSlider.value = _health;
        }
        else Debug.LogWarning("No health assigned to player");
        
        BulletBehaviour.setDamage(_damage);
        BulletBehaviour.setBulletSpeed(_shootSpeed);

        _baseArmor = _maxHealth;
        _baseDamage = _damage;
        _basePlayerSpeed = _moveSpeed;
        _baseShootSpeed = _shootSpeed;
    }

    void Update()
    {
        // Movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        // Apply movement directly to the Rigidbody2D
        _rb.linearVelocity = moveDirection * _moveSpeed;
        updateStats();
    }
    
    public float getCoinBalance() { return _coinBalance; }
    
    public void setCoinBalance(float value) { _coinBalance = value; }

    public bool makePurchase(float amount)
    {
        if (_coinBalance < amount) return false;
        _coinBalance -= amount;
        updateCoinBalance();
        return true;
    }

    public void addCoins(float amount)
    {
        _coinBalance += amount;
        updateCoinBalance();
    }

    public void updateCoinBalance()
    {
        string formatted = _coinBalance.ToString("F2");
        _coinText.text = "Coins: $" + formatted;
    }

    public void Damage(float amount)
    {
        if (_health <= 0)
        {
            GameOver();
            return;
        }
        _health -= amount;
        updateHealthSlider();
        StartCoroutine(DamageFlash());
    }
    
    private IEnumerator DamageFlash()
    {
        float elapsedTime = 0f;
    
        while (elapsedTime < flashDuration)
        {
            _sr.color = Color.Lerp(originalColor, damageColor, elapsedTime / flashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
        _sr.color = damageColor; 

        elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            _sr.color = Color.Lerp(damageColor, originalColor, elapsedTime / flashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _sr.color = originalColor;
    }

    private void GameOver()
    {
        GameOverScreen.Instance.Enabled = true;
    }
    public void Heal(float amount)
    {
        _health += amount;
        updateHealthSlider();
    }

    public void updateHealthSlider()
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = _health;
        }
    }

    public void respawnPlayer()
    {
        _health = _maxHealth;
        _coinBalance *= _lossRate;
        updateCoinBalance();
        updateHealthSlider();
    }
    
}