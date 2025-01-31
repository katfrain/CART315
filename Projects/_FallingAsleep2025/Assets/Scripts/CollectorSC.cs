using UnityEngine;

public class CollectorSC : MonoBehaviour
{
    public float xLoc, yLoc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xLoc = 0;
        yLoc = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Debug.Log("Left");
            xLoc -= 0.1f;
        }

        if (Input.GetKey(KeyCode.X))
        {
            Debug.Log("Right");
            xLoc += 0.1f;
        }

        this.transform.position = new Vector3(xLoc, yLoc, 0);

        void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(other.gameObject);
        }
            
    }
}
