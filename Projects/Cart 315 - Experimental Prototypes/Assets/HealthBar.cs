using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float damage;
    public GameObject player;
    public waterDrop waterDrop;
    
    private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1;
    }

    private void OnEnable()
    {
        if (waterDrop != null)
        {
            waterDrop.hitPlayer1.AddListener(player1hit);
            waterDrop.hitPlayer2.AddListener(player2hit);
        }
    }

    private void OnDisable()
    {
        if (waterDrop != null)
        {
            waterDrop.hitPlayer1.RemoveListener(player1hit);
            waterDrop.hitPlayer2.RemoveListener(player2hit);
        }
    }

    private void player1hit()
    {
        if (player != null && player.CompareTag("Player1"))
        {
            slider.value -= damage;
        }
    }
    private void player2hit()
    {
        if (player != null && player.CompareTag("Player2"))
        {
            slider.value -= damage;
        }
    }
}
