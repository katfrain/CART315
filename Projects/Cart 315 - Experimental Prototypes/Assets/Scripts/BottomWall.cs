using System;
using UnityEngine;
using UnityEngine.Events;

public class BottomWall : MonoBehaviour
{
    public UnityEvent potatoEntered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Potato" || other.gameObject.name == "Potato(Clone)")
        {
            Destroy(other.gameObject);
        }
        potatoEntered.Invoke();
    }
}
