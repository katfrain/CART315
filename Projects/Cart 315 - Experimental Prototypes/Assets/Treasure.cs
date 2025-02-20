using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    public LayerMask Enemy;
    public float damageAmount = 2f;
    public float damageInterval = 1f;
    public float totalHealth = 100f;
    public TK_HealthBar healthBar;
    public ParticleSystem damageParticles;
    
    private ParticleSystem damageParticlesInstance;
    private IEnumerator coroutine;
    private IEnumerator gameOverCoroutine;
    private int enemiesTouching = 0;

    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((Enemy & (1 << collision.gameObject.layer)) > 0)
        {
            Debug.Log("Collision entered");
            enemiesTouching++;
            if (coroutine == null)
            {
                Debug.Log("Starting Coroutine ...");
                coroutine = takeDamage();
                StartCoroutine(coroutine);
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((Enemy & (1 << collision.gameObject.layer)) > 0)
        {
            Debug.Log("Collision exited");
            enemiesTouching--;
            if (enemiesTouching <= 0 && coroutine != null)
            {
                Debug.Log("Stopping Coroutine ...");
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }

    private IEnumerator takeDamage()
    {
        while (true)
        {
            Debug.Log(gameObject.name + " taking damage");
            if (totalHealth <= 0)
            {
                SpawnDamageParticles();
                TK_MenuScreen.GameOver = true;
                SceneManager.LoadScene(4);
                Destroy(gameObject);
                Destroy(healthBar.gameObject);
                Destroy(damageParticlesInstance.gameObject, 2f);
                yield break;
            }
            totalHealth -= damageAmount;
            healthBar.takeDamage(damageAmount);
            SpawnDamageParticles();
            yield return new WaitForSeconds(damageInterval);
        }
    }
    
    private void SpawnDamageParticles()
    {
        damageParticlesInstance = Instantiate(damageParticles, transform.position, Quaternion.identity);
        Destroy(damageParticlesInstance.gameObject, 2f);
    }
}
