using UnityEngine;

public class EndZone : MonoBehaviour
{
    public Player player;
    private bool inArea;
    private bool hitSucessfully;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inArea = false;
        hitSucessfully = false;
        player.successfulHitEvent.AddListener(hit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        inArea = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!hitSucessfully)
        {
            Debug.Log("Game Over!");
        }
        hitSucessfully = false;
    }

    void hit()
    {
        hitSucessfully = true;
    }
}
