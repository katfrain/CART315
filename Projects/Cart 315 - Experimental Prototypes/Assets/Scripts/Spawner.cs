using UnityEngine;

public class Spawner : MonoBehaviour
{
   // Spawner object spawns in instances of note object using two arrays spawnTimes and spawnBools to control when each not drops
    public GameObject note;
    private float elapsedTime;
    public float[] spawnTimes;
    public bool[] spawnBools;
    void Start()
    {
        spawnTimes = new float[] {0, 1.5f, 2.2f, 4f, 5f };
        spawnBools = new bool[spawnTimes.Length];
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        // For each spawn time in the array, check if its time to drop (elapsed time > spawn time) and check if it has already been dropped (bool is true)
        for (int i = 0; i < spawnTimes.Length; i++)
        {
            if (!spawnBools[i] && elapsedTime >= spawnTimes[i])
            {
                SpawnCircle();
                spawnBools[i] = true;  // Mark this time as used
            }
        }
    }

    // Currently I have the notes spawn in a random lane, as the focus is on the rhythm. I would change this if I wanted specific charts for each song 
    private void SpawnCircle()
    {
        int randomNumber = Random.Range(1, 5);
        switch (randomNumber)
        {
           case 1:
               SpawnNoteLane1();
               break;
           case 2:
               SpawnNoteLane2();
               break;
           case 3:
               SpawnNoteLane3();
               break;
           case 4:
               SpawnNoteLane4();
               break;
        }
    }
    void SpawnNoteLane1()
    {
        Instantiate(note, new Vector3(-2.5f, 5.5f, 0f), Quaternion.identity);
    }
    void SpawnNoteLane2()
    {
        Instantiate(note, new Vector3(-0.5f, 5.5f, 0f), Quaternion.identity);
    }
    void SpawnNoteLane3()
    {
        Instantiate(note, new Vector3(1.5f, 5.5f, 0f), Quaternion.identity);
    }
    void SpawnNoteLane4()
    {
        Instantiate(note, new Vector3(3.5f, 5.5f, 0f), Quaternion.identity);
    }
}
