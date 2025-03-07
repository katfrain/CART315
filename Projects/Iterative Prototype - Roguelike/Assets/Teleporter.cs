using System;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination; // The destination to teleport to
    [SerializeField] private Camera camera;
    [SerializeField] private RoomGenerator roomGenerator;
    
    public enum Direction { left, right, up, down };
    private Direction _direction;
    private float _height;
    private float _width;
    
    void Awake()
    {
        camera = Camera.main;  // Finds the main camera
        roomGenerator = FindObjectOfType<RoomGenerator>(); // Finds RoomGenerator in the scene
    }

    
    public Direction direction
    {
        get { return _direction; }
        set
        {
            _direction = value;
            _height = roomGenerator.height;
            _width = roomGenerator.width;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Temporarily stop the player's movement to avoid glitches
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            
            if (rb != null)
            {
                Vector2 velocity = rb.linearVelocity;
                rb.linearVelocity = Vector2.zero; 
                collision.gameObject.transform.position = destination.position;
                rb.linearVelocity = velocity;
            }

            if (camera != null)
            {
                switch (_direction)
                {
                    case Direction.left:
                        camera.transform.position = new Vector3(camera.transform.position.x - _width, camera.transform.position.y, camera.transform.position.z);
                        break;
                    case Direction.right:
                        camera.transform.position = new Vector3(camera.transform.position.x + _width, camera.transform.position.y, camera.transform.position.z);
                        break;
                    case Direction.up:
                        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - _height, camera.transform.position.z);
                        break;
                    case Direction.down:
                        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + _height, camera.transform.position.z);
                        break;
                }
            }
        }
    }
}
