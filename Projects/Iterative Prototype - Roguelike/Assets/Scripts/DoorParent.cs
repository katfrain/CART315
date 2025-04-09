using UnityEngine;

public class DoorParent : MonoBehaviour
{
    protected Vector3 _position;
    protected float _width;
    protected float _height;
    protected float _direction;
    protected Room _currentRoom;
    protected Room _nextRoom;
    protected bool isLocked = false;
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
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

    public virtual void Lock()
    {
        isLocked = true;
        spriteRenderer.color = new Color(118f / 255f, 41f / 255f, 37f / 255f, 1f);
    }

    public virtual void Unlock()
    {
        isLocked = false;
        spriteRenderer.color = originalColor;
    }

}