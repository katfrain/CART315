using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaserSpawn : MonoBehaviour
{

    [SerializeField] private int maxChasers = 3;
    [SerializeField] private float maxTimeBetweenSpawns = 3f;
    [SerializeField] private Chaser chaserPrefab;
    
    private int chasersSpawned = 0;
    private List<Chaser> activeChasers = new List<Chaser>();
    private Room parentRoom;
    private bool finishedSpawning = false;
    private SpriteRenderer sr;
    private Color finishedSpawningColor = new Color(0.101f, 0.06f, 0.16f, 1f);

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void Activate(Room room)
    {
        Debug.Log(this + "Activate Chaser Spawn");
        parentRoom = room;
        StartCoroutine(SpawnChasers());
    }

    public void Deactivate()
    {
        StopAllCoroutines();
    }
    

    IEnumerator SpawnChasers()
    {
        Debug.Log(this + "Spawn Chasers");
        while (chasersSpawned < maxChasers)
        {
            float randomTime = Random.Range(1f, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(randomTime);
            Chaser chaser = Instantiate(chaserPrefab, transform.position, Quaternion.identity);
            chaser.Initialize(this, parentRoom); 

            activeChasers.Add(chaser);
            chasersSpawned++;
        }

        finishedSpawning = true;
        StartCoroutine(LerpColor(sr, sr.color, finishedSpawningColor, 1.5f));
        Debug.Log("All chaser spawned, attempting to unlock room");
        parentRoom?.TryUnlockDoors(); 
    }

    public bool IsFinishedSpawning() => finishedSpawning;

    public bool AreAllEnemiesDead()
    {
        Debug.Log(activeChasers.All(c => c == null));
        return activeChasers.All(c => c == null); 
    }

    public void NotifyChaserDeath(Chaser chaser)
    {
        activeChasers.Remove(chaser); 
        parentRoom?.TryUnlockDoors(); 
    }
    IEnumerator LerpColor(SpriteRenderer renderer, Color startColor, Color targetColor, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            renderer.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        renderer.color = targetColor; 
    }
    
    
}
