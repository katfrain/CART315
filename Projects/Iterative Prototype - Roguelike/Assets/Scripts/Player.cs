using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float _moveSpeed = 5f; // Player movement speed
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private float _health = 100f;
    [SerializeField] private Slider _healthSlider;
    private Rigidbody2D _rb;
    private float _coinBalance;

    public static Player Instance;

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
        _rb = GetComponent<Rigidbody2D>(); 
        _coinBalance = 0;
        updateCoinBalance();
        if (_healthSlider != null)
        {
            _healthSlider.maxValue = _health;
            _healthSlider.value = _health;
        }
        else Debug.LogWarning("No health assigned to player");
    }

    void Update()
    {
        // Movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        // Apply movement directly to the Rigidbody2D
        _rb.linearVelocity = moveDirection * _moveSpeed;
    }
    
    public float getCoinBalance() { return _coinBalance; }

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
        _health -= amount;
        updateHealthSlider();
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
    
}