using UnityEngine;
using UnityEngine.UI;

public class Excavator : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 150;
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private GameObject[] drops;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Canvas canvas;

    private GameObject coinInst;
    private float health;
    private ParticleSystem damageParticlesInstance;

    void Start()
    {
        health = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }
        hideCanvas();
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            SpawnDamageParticles();
            Destroy(gameObject);
            Destroy(damageParticlesInstance.gameObject, 2f);
        }
        SpawnDamageParticles();
        UpdateHealthSlider();
    }
    private void SpawnDamageParticles()
    {
        damageParticlesInstance = Instantiate(damageParticles, transform.position, Quaternion.identity);
        Destroy(damageParticlesInstance.gameObject, 2f);
    }
    
    void UpdateHealthSlider()
    {
        healthSlider.value = health;
    }

    public void dropReward()
    {
        foreach (GameObject drop in drops)
        {
            coinInst = Instantiate(drop, transform.position, Quaternion.identity);
        }
    }

    public void hideCanvas()
    {
        canvas.enabled = false;
    }

    public void showCanvas()
    {
        canvas.enabled = true;
    }
    
}