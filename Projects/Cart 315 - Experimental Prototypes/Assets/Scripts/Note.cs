using UnityEngine;

public class Note : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float fallDuration; 
    private float elapsedTime;
    private Vector3 velocity; 
    
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition - new Vector3(0, 11f, 0); // End position = start position - 11 on the Y axis
        fallDuration = 2f; // Fall duration in seconds
        velocity = (endPosition - startPosition) / fallDuration; // Speed calculated with fall duration
    }
    
    void Update()
    {
        if (elapsedTime < fallDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position += velocity * Time.deltaTime;  
        }
        else 
        {
            transform.position = endPosition;  // Make sure it snaps exactly to the end position
            Destroy(this.gameObject);
        }
    }
}
