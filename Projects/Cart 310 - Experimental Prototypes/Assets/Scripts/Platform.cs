using UnityEngine;

public class Platform : MonoBehaviour
{
    private float xPos;
    private float zRotation;

    public float platformSpeed;
    public float platformRotationSpeed;
    public KeyCode leftKey, rightKey;
    public KeyCode leftRotation, rightRotation;
    public float leftWall, rightWall;
    public float maxRotation;
    public float minRotation;
    public AudioClip bounceSound;
    public AudioSource audioSource;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftKey) && xPos >= leftWall)
        {
            xPos -= platformSpeed;
        }
        if (Input.GetKey(rightKey) && xPos <= rightWall)
        {
            xPos += platformSpeed;
        }

        if (Input.GetKey(leftRotation) && zRotation <= maxRotation)
        {
            zRotation += platformRotationSpeed;
        }
        if (Input.GetKey(rightRotation) && zRotation >= minRotation)
        {
            zRotation -= platformRotationSpeed;
        }
        
        transform.localPosition = new Vector3(xPos, transform.position.y, 0);
        transform.localRotation = Quaternion.Euler(0, 0, zRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlaySound(bounceSound);
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
