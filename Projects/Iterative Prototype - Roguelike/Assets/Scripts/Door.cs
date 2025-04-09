using System;
using UnityEngine;

public class Door : DoorParent
{
    [SerializeField] private Teleporter entryTeleporter, exitTeleporter;

    public override float Direction
    {
        get { return _direction; }
        set
        {
            _direction = value;
            this.transform.rotation = Quaternion.Euler(0, 0, _direction);
            switch (_direction)
            {
                case 0:
                    entryTeleporter.direction = Teleporter.Direction.left;
                    exitTeleporter.direction = Teleporter.Direction.right;
                    break;
                case 90:
                    entryTeleporter.direction = Teleporter.Direction.up;
                    exitTeleporter.direction = Teleporter.Direction.down;
                    break;
                case 180:
                    entryTeleporter.direction = Teleporter.Direction.right;
                    exitTeleporter.direction = Teleporter.Direction.left;
                    break;
                case 270:
                    entryTeleporter.direction = Teleporter.Direction.down;
                    exitTeleporter.direction = Teleporter.Direction.up;
                    break;
            }
        }
    }

    public override void Lock()
    {
        base.Lock();
        entryTeleporter.Lock();
        exitTeleporter.Lock();
    }

    public override void Unlock()
    {
        base.Unlock();
        entryTeleporter.Unlock();
        exitTeleporter.Unlock();
    }
}