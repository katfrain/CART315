using UnityEngine;

public class DoorLevelSpawner : DoorParent
{
    [SerializeField] private Teleporter entryTeleporter;

    public override float Direction
    {
        get { return _direction; }
        set
        {
            _direction = value;
            this.transform.rotation = Quaternion.Euler(0, 0, _direction);
            switch (_direction)
            {
                case 180:
                    entryTeleporter.direction = Teleporter.Direction.right;
                    break;
                case 270:
                    entryTeleporter.direction = Teleporter.Direction.up;
                    break;
                case 0:
                    entryTeleporter.direction = Teleporter.Direction.left;
                    break;
                case 90:
                    entryTeleporter.direction = Teleporter.Direction.down;
                    break;
            }
        }
    }
    
    public override void Lock()
    {
        base.Lock();
        entryTeleporter.Lock();
    }

    public override void Unlock()
    {
        base.Unlock();
        entryTeleporter.Unlock();
    }
}
