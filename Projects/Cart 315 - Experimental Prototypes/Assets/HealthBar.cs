using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public float damage;
    public HosePlayer player;
    public Player PlayerSelection;
    public GameObject GameOverText;

    public enum Player
    {
        Player1,
        Player2,
    }

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1;
    }

    private void OnEnable()
    {
        if (player != null)
        {
            Debug.Log("HealthBar subscribed to player events");
            player.hitPlayer1.AddListener(player1hit);
            player.hitPlayer2.AddListener(player2hit);
            GameOverText.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.hitPlayer1.RemoveListener(player1hit);
            player.hitPlayer2.RemoveListener(player2hit);
        }
    }

    void Update()
    {
        if (slider.value <= 0)
        {
            string winningPlayer;
            if (PlayerSelection == Player.Player1) winningPlayer = "Player 2";
            else winningPlayer = "Player 1";
            GameOverText.GetComponent<TMP_Text>().text = "Game Over!\n" + winningPlayer + " Wins!";
            GameOverText.SetActive(true);
            Destroy(player.gameObject);
        }
    }

    private void player1hit()
    {
        Debug.Log("Player 1 hit! Health is decreasing.");
        if (PlayerSelection == Player.Player1)
        {
            slider.value -= damage;
        }
    }

    private void player2hit()
    {
        Debug.Log("Player 2 hit! Health is decreasing.");
        if (PlayerSelection == Player.Player2)
        {
            slider.value -= damage;
        }
    }
}