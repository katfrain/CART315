using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 _position;
    private float _width;
    private float _height;
    private float _direction;
    private Room _currentRoom;
    private Room _nextRoom;

    public Vector3 Position
    {
        get { return _position; }
        set
        {
            _position = value; 
            this.transform.position = _position;
        }
    }

    public float Width
    {
        get { return _width; }
        set
        {
            _width = value;
            this.transform.localScale = new Vector3(_width, _height, 0);
        }
    }

    public float Height
    {
        get { return _height; }
        set
        {
            _height = value;
            this.transform.localScale = new Vector3(_width, _height, 0);
        }
    }

    public float Direction
    {
        get { return _direction; }
        set
        {
            _direction = value; 
            this.transform.rotation = Quaternion.Euler(0, 0, _direction);
        }
    }

    public Room CurrentRoom
    {
        get { return _currentRoom; }
        set { _currentRoom = value; }
    }

    public Room NextRoom
    {
        get { return _nextRoom; }
        set { _nextRoom = value; }
    }

    public void setLocation(Vector3 position, float width, float height, float direction)
    {
        _position = position;
        _width = width;
        _height = height;
        _direction = direction;
    }

}