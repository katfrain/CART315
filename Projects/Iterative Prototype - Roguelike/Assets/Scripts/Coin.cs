using System;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Player player;
    private enum CoinValue { One = 1, Five = 5, Ten = 10, TwentyFive = 25 }
    [SerializeField] private CoinValue coinValue = CoinValue.One;
    private SpriteRenderer coinSprite;

    void Start()
    {
        player = Player.Instance;
        coinSprite = GetComponent<SpriteRenderer>();
        switch (coinValue)
        {
            case CoinValue.One:
                coinSprite.color = new Color(120f / 255, 69f/ 255, 28f/ 255, 1f);
                break;
            case CoinValue.Five:
                coinSprite.color = new Color(145f/ 255, 145f/ 255, 145f/ 255, 1f);
                break;
            case CoinValue.Ten:
                coinSprite.color = new Color(255f / 255, 182f / 255, 105f / 255, 1f);
                break;
            case CoinValue.TwentyFive:
                coinSprite.color = new Color(217f / 255, 251f / 255, 255f / 255, 1f);
                break;
        }
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
        player.addCoins((float)coinValue);
    }
    
}
