using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Coin : MonoBehaviour
{
    private Player player;
    public enum CoinValue { One = 1, Five = 5, Ten = 10, TwentyFive = 25 }
    [SerializeField] private CoinValue coinValue = CoinValue.One;
    [SerializeField] private SpriteRenderer coinSprite;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private float scaleRate = 0.3f;
    [SerializeField] private static int scale = 0;
    [SerializeField] private int value = 1;

    void Start()
    {
        player = Player.Instance;
        if (coinSprite == null)
        {
            Debug.LogError("SpriteRenderer is missing on the Coin object. Please add it to the prefab.");
        }

        setCoinValue(coinValue); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pickUpCoin();
            Destroy(this.gameObject);
        }
    }

    void pickUpCoin()
    {
        player.addCoins(value);
    }

    public void setCoinValue(CoinValue cv)
    {
        coinValue = cv;
        switch (cv)
        {
            case CoinValue.One:
                coinSprite.color = new Color(120f / 255, 69f/ 255, 28f/ 255, 1f);
                value = 1 + (int)(1 * scaleRate * scale);
                break;
            case CoinValue.Five:
                coinSprite.color = new Color(145f/ 255, 145f/ 255, 145f/ 255, 1f);
                value = 5 + (int)(5 * scaleRate * scale);
                break;
            case CoinValue.Ten:
                coinSprite.color = new Color(255f / 255, 182f / 255, 105f / 255, 1f);
                value = 10 + (int)(10 * scaleRate * scale);
                break;
            case CoinValue.TwentyFive:
                coinSprite.color = new Color(217f / 255, 251f / 255, 255f / 255, 1f);
                value = 25 + (int)(25 * scaleRate * scale);
                break;
        }
        coinText.text = value.ToString();
    }

    public static void upgradeScale()
    {
        scale++;
    }

    public static void resetScale()
    {
        scale = 0;
    }
    
}
