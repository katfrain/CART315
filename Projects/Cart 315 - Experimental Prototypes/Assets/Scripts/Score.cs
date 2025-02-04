using System;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public BottomWall BottomWall;
    public PotatoSpawner PotatoSpawner;
    public TextMeshProUGUI ScoreText;
    public AudioClip spawnSound;
    public AudioClip loseSound;
    public AudioSource audioSource;
    private int score;
    private int highScore;

    void Start()
    {
        score = 1;
        highScore = 1;
        ScoreText.text = $"score: {score}\nhighScore: {highScore}";
        
    }
    private void OnEnable()
    {
        if (BottomWall != null)
        {
            BottomWall.potatoEntered.AddListener(potatoEntered);
        }

        if (PotatoSpawner != null)
        {
            PotatoSpawner.potatoSpawned.AddListener(potatoSpawned);
        }
    }

    private void OnDisable()
    {
        if (BottomWall != null)
        {
            BottomWall.potatoEntered.RemoveListener(potatoEntered);
        }
        if (PotatoSpawner != null)
        {
            PotatoSpawner.potatoSpawned.RemoveListener(potatoSpawned);
        }
    }

    private void potatoEntered()
    {
        Debug.Log("Potato Entered");
        score--;
        ScoreText.text = $"score: {score}\nhighScore: {highScore}";
        PlaySound(loseSound);
    }

    private void potatoSpawned()
    {
        Debug.Log("Potato Spawned");
        score++;
        if (score >= highScore)
        {
            highScore = score;
        }
        ScoreText.text = $"score: {score}\nhighScore: {highScore}";
        PlaySound(spawnSound);
    }
    
    private void PlaySound(AudioClip clip)
    {
        Debug.Log("Playing Sound");
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); 
        }
    }
}
