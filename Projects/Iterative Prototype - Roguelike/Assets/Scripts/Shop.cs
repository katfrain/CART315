using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private float cost;
    [SerializeField] private string shopName;
    [SerializeField] private int inventory;
    [SerializeField] private float priceIncrease = 1.75f;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private ShopType shopType;

    private enum ShopType  {
        ShootSpeed,
        Damage,
        PlayerSpeed,
        Armor
    }
    private Player player;

    private bool _visible = true;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        player = Player.Instance;
        updateInventory();
        updateText();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Purchase();
        }
    }

    private void updateInventory()
    {
        switch (shopType)
        {
            case ShopType.PlayerSpeed:
                inventory = player.getPlayerSpeedMaxLevel() - 1;
                break;
            case ShopType.Armor:
                inventory = player.getArmorMaxLevel() - 1;
                break;
            case ShopType.Damage:
                inventory = player.getDamageMaxLevel() - 1;
                break;
            case ShopType.ShootSpeed:
                inventory = player.getShootSpeedMaxLevel() - 1;
                break;
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
        bool sucess = false;
        switch (shopType)
        {
            case ShopType.PlayerSpeed:
                if (player.upgradePlayerSpeed(cost)) { sucess = true; }
                break;
            case ShopType.Armor:
                if (player.upgradeArmor(cost)) { sucess = true; }
                break;
            case ShopType.Damage:
                if (player.upgradeDamage(cost)) { sucess = true; }
                break;
            case ShopType.ShootSpeed:
                if (player.upgradeShootSpeed(cost)) { sucess = true; }
                break;
        }
        if (sucess)
        {
            inventory--;
            increasePrice();
            updateText();
        }
        else Debug.Log("Not enough money");
    }

    void updateText()
    {
        if (inventory == 0)
        {
            costText.text = shopName + "\nMax Level!" ;
        }
        else
        {
            string formatted = cost.ToString("F2");
            costText.text = shopName + "\nCost: $" + formatted + "\nInventory: " + inventory;
        }
    }

    void increasePrice()
    {
        cost *= priceIncrease;
    }
    
}
