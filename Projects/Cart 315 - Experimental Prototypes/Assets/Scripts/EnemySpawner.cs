using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float timeBetweenSpawn = 3f;
    public GameObject enemy;

    private GameObject enemyInst;
    private IEnumerator coroutine;
    private Vector3 spawnPosition;
    private float xPos;
    private float yPos;
    private void Start()
    {
        coroutine = SpawnEnemy();
        StartCoroutine(coroutine);
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            int spawnSide = Random.Range(0, 4);
            switch (spawnSide)
            {
                case 0: // left side
                    xPos = -14;
                    yPos = Random.Range(-4.5f, 4.5f);
                    break;
                case 1: // top side
                    xPos = Random.Range(-13f, 13f);
                    yPos = 5.6f;
                    break;
                case 2: // right side
                    xPos = 14;
                    yPos = Random.Range(-4.5f, 4.5f);
                    break;
                case 3: // bottom side
                    xPos = Random.Range(-13f, 13f);
                    yPos = 5.6f;
                    break;
            }
            spawnPosition = new Vector3(xPos, yPos, 0);
            enemyInst = Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }
}
