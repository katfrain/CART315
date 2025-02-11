using UnityEngine;

public class HosePlayer : MonoBehaviour
{
     public KeyCode leftKey, rightKey;
     public float playerSpeed;
     public float leftWall, rightWall;
     
     private float xPos;
         
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftKey) && xPos >= leftWall)
        {
            xPos -= playerSpeed * Time.deltaTime;;
        }
        if (Input.GetKey(rightKey) && xPos <= rightWall)
        {
            xPos += playerSpeed * Time.deltaTime;;
        }
        transform.localPosition = new Vector3(xPos, transform.position.y, 0);
    }
}
