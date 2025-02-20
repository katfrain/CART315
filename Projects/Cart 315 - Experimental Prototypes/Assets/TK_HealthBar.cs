using UnityEngine;
using UnityEngine.UIElements;

public class TK_HealthBar : MonoBehaviour
{
    public float currentHealth = 100f;
    public float width = 100f;
    public float height = 50;
    public RectTransform healthBar;
    void Start()
    {

    }
    
    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.sizeDelta = new Vector2(currentHealth, height);
        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
