using UnityEngine;
using UnityEngine.Events;

public class HosePlayer : MonoBehaviour
{
    public KeyCode leftKey, rightKey;
    public float playerSpeed;
    public float leftWall, rightWall;
    
    public UnityEvent hitPlayer1 = new UnityEvent();
    public UnityEvent hitPlayer2 = new UnityEvent();
    
    private float xPos;

    void Start()
    {
        xPos = transform.position.x;
    }

    void Update()
    {
        if (Input.GetKey(leftKey) && xPos >= leftWall)
        {
            xPos -= playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(rightKey) && xPos <= rightWall)
        {
            xPos += playerSpeed * Time.deltaTime;
        }
        transform.localPosition = new Vector3(xPos, transform.position.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Waterdrop"))
        {
            Debug.Log($"{gameObject.name} was hit!");

            if (gameObject.CompareTag("Player1"))
                hitPlayer1.Invoke();
            else if (gameObject.CompareTag("Player2"))
                hitPlayer2.Invoke();

            Destroy(collision.gameObject);
        }
    }
}