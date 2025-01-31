using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float ballSpeed;
    private int[] directions = {
        -1, 1
    };

    private int score1;
    private int score2;

    private int hDir, yDir;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(Launch());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D wall)
    {
        Debug.Log(wall.gameObject.name);
        if (wall.gameObject.name == "RightWall")
        {
            Reset();
            score1++;
            Debug.Log("Player 2 score: " + score1);
        }

        if (wall.gameObject.name == "LeftWall")
        {
            Reset();
            score2++;
            Debug.Log("Player 2 score: " + score2);
        }
    }

    private IEnumerator Launch()
    {
        hDir = directions[Random.Range(0, directions.Length)];
        yDir = directions[Random.Range(0, directions.Length)];
        
        yield return new WaitForSeconds(1);
        rb.AddForce(transform.right * hDir * ballSpeed);
        rb.AddForce(transform.up * yDir * ballSpeed);
    }

    private void Reset()
    {
        rb.linearVelocity = new Vector2(0, 0);
        this.transform.position = new Vector3(0, 0, 0);
        StartCoroutine(Launch());   
    }
}
