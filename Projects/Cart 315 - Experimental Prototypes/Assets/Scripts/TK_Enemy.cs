using TMPro;
using UnityEngine;

public class TK_Enemy : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    public Vector3 destination;
    public float speed = 5f; 
    public float maxHealth = 3f;
    public ParticleSystem damageParticles;
    public TextMeshProUGUI scoreText;
    
    public static int score = 0;
    private ParticleSystem damageParticlesInstance;
    private float currentHealth;

    void Start()
    {
        scoreText = FindObjectOfType<TextMeshProUGUI>();
        scoreText.text = "SCORE: " + score;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, destination, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            score++;
            scoreText.text = "SCORE: " + score;
            SpawnDamageParticles();
            Destroy(gameObject);
            Destroy(damageParticlesInstance.gameObject, 2f);
        }
        Vector3 newScale = new Vector3(0.15f, 0.15f, 1);
        transform.localScale = transform.localScale - newScale;
        SpawnDamageParticles();
    }

    private void SpawnDamageParticles()
    {
        damageParticlesInstance = Instantiate(damageParticles, transform.position, Quaternion.identity);
        Destroy(damageParticlesInstance.gameObject, 2f);
    }
}
