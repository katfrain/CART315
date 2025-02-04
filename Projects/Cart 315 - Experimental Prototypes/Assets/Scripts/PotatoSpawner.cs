using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PotatoSpawner : MonoBehaviour
{
    public float spawnIntervalDuration;
    public GameObject potatoPrefab;
    public Sprite[] potatoBaseSprites;
    public Sprite[] potatoHitSprites;
    
    public UnityEvent potatoSpawned;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnPotatoEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator spawnPotatoEnumerator()
    {
        Debug.Log("Starting enumerator");
        yield return new WaitForSeconds(spawnIntervalDuration);
        spawnPotato();
    }

    private void spawnPotato()
    {
        Debug.Log("Spawning potato");
        int potatoTypes = Random.Range(0, potatoBaseSprites.Length);
        float xPos = Random.Range(-8f, 8f);
        GameObject newPotato = Instantiate(potatoPrefab, new Vector3(xPos, 3f, 0f), Quaternion.identity);
        
        Potato potatoScript = newPotato.GetComponent<Potato>();

        if (potatoScript != null)
        {
            potatoScript.baseSprite = potatoBaseSprites[potatoTypes];
            potatoScript.hitSprite = potatoHitSprites[potatoTypes];
            
            SpriteRenderer sr = newPotato.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = potatoScript.baseSprite;
            }
            potatoSpawned.Invoke();
        }
        else
        {
            Debug.LogError("Potato script not found on instantiated prefab!");
        }
        StartCoroutine(spawnPotatoEnumerator());
    }
}
