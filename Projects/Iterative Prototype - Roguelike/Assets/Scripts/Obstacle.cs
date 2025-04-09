using UnityEngine;

public class Obstacle : MonoBehaviour, IDamageable
{
    private SpriteRenderer sr;
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private ParticleSystem ps;
    private float health;
    private ParticleSystem psInst;

    void Start()
    {
        health = maxHealth;
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            SpawnDamageParticles();
            Destroy(gameObject);
            Destroy(psInst.gameObject, 2f);
        }
        UpdateColorBasedOnHealth();
        SpawnDamageParticles();
    }
    
    private void SpawnDamageParticles()
    {
        psInst = Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(psInst.gameObject, 2f);
    }
    
    private void UpdateColorBasedOnHealth()
    {
        float healthPercent = health / maxHealth;
        
        Color darkBrown = new Color(0.29f, 0.2f, 0.1f);   // Dark, earthy brown
        Color lightBrown = new Color(0.76f, 0.60f, 0.42f);  // Light tan/brown
        
        Color newColor = Color.Lerp(darkBrown, lightBrown, healthPercent);
        newColor.a = sr.color.a; 

        sr.color = newColor;
    }

}
