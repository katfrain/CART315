using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination; // Set in Unity Inspector
    [SerializeField] private bool levelGenerator;
    
    public enum Direction { left, right, up, down };
    [SerializeField] private Direction _direction;

    public Direction direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entering teleporter, is generator: " + levelGenerator);
        Camera camera = GameManager.Instance.MainCamera;
        RoomGenerator roomGenerator = GameManager.Instance.RoomGenerator;

        if (camera != null && roomGenerator != null)
        {
            float height = roomGenerator.height;
            float width = roomGenerator.width;

            Debug.Log("Room Width: " + width + ", Room Height: " + height);
            
            switch (_direction)
            {
                case Direction.left:
                    camera.transform.position += new Vector3(-width, 0, 0);
                    break;
                case Direction.right:
                    camera.transform.position += new Vector3(width, 0, 0);
                    break;
                case Direction.up:
                    camera.transform.position += new Vector3(0, -height, 0);
                    break;
                case Direction.down:
                    camera.transform.position += new Vector3(0, height, 0);
                    break;
            }
            Debug.Log("Camera Moved to: " + camera.transform.position);
        }

        if (collision.gameObject.CompareTag("Player") && !levelGenerator)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.linearVelocity;
                rb.linearVelocity = Vector2.zero;

                if (destination != null)
                {
                    collision.transform.position = destination.position;
                }
                else
                {
                    Debug.LogError("Destination is null! Assign a destination in the Inspector.");
                }

                rb.linearVelocity = velocity;
            }
        }

        if (levelGenerator && roomGenerator != null)
        {
            Room.DoorType doorDirection = Room.DoorType.Bottom;
            switch (_direction)
            {
                case Direction.left:
                    doorDirection = Room.DoorType.Left;
                    break;
                case Direction.right:
                    doorDirection = Room.DoorType.Right;
                    break;
                case Direction.up:
                    doorDirection = Room.DoorType.Top;
                    break;
                case Direction.down:
                    doorDirection = Room.DoorType.Bottom;
                    break;
            }

            Debug.Log("Generating new level with door direction: " + doorDirection);
            roomGenerator.GenerateNewLevel(doorDirection);
            Debug.Log("New level generated.");
        }
        
    }
}
