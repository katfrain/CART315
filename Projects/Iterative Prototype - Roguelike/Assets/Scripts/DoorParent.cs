using UnityEngine;

public class DoorParent : MonoBehaviour
{
    protected Vector3 _position;
    protected float _width;
    protected float _height;
    protected float _direction;
    protected Room _currentRoom;
    protected Room _nextRoom;

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

    public virtual float Direction
    {
        get { return _direction; }
        set
        {
            _direction = value; 
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