using Unity.VisualScripting;
using UnityEngine;

public class CircleSC : MonoBehaviour
{
    public float xLoc, yLoc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xLoc = this.transform.position.x;
        yLoc = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        yLoc -= 0.1f;
        this.transform.position = new Vector3(xLoc, yLoc, 0);
    }
}
