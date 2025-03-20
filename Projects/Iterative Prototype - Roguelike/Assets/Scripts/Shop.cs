using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private float cost;
    [SerializeField] private int inventory;
    [SerializeField] private float priceIncrease = 1.25f;
    [SerializeField] private TMP_Text costText;
    private Player player;

    private bool _visible = true;
    
    public static Shop Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        player = Player.Instance;
        updateText();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Purchase();
        }
    }

    public bool Visible
    {
        get { return _visible; }
        set
        {
            _visible = value;
            gameObject.SetActive(_visible);
        }
    }
    void Purchase()
    {
        if (inventory == 0)
        {
            Debug.Log("No inventory");
            return;
        }
        if (player.makePurchase(cost))
        {
            inventory--;
            increasePrice();
            updateText();
        }
        else Debug.Log("Not enough money");
    }

    void updateText()
    {
        string formatted = cost.ToString("F2");
        costText.text = "Shop\nCost: $" + formatted + "\nInventory: " + inventory;
    }

    void increasePrice()
    {
        cost *= priceIncrease;
    }
    
}
