using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent successfulHitEvent;
    private int pos;
    private float elapsedTime;
    private bool inArea;
    private bool noteWasHit;
    
    void Start()
    {
        this.transform.position = new Vector3(-2.5f, -3.5f, 0);
        pos = 1;
        noteWasHit = false;
    }
    
    void Update()
    {
        elapsedTime += Time.deltaTime;
        // Move between the 4 lanes with ASDF
        if (Input.GetKeyDown(KeyCode.A))
        {
            pos = 1;
            this.transform.position = new Vector3(-2.5f, -3.5f, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pos = 2;
            this.transform.position = new Vector3(-0.5f, -3.5f, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pos = 3;
            this.transform.position = new Vector3(1.5f, -3.5f, 0);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            pos = 4;
            this.transform.position = new Vector3(3.5f, -3.5f, 0);
        }
        // If the player is in the correct area and the player hit enter at the right time, the hit was successful, send event to endzone to not trigger game over.
        if (Input.GetKeyDown(KeyCode.Return) && inArea)
        {
            Debug.Log("Note Hit!");
            successfulHitEvent?.Invoke();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        inArea = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        inArea = false;
    }
}


