using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomTypes
    {
        Entry = 1,
        Easy = 2,
        Medium = 3,
        Hard = 4,
    }

    public enum DoorType
    {
        Top,
        Bottom,
        Left,
        Right,
        Exit,
    }

    // Private/Protected Fields
    protected RoomTypes _roomType;
    protected int _numOfDoors;
    protected List<DoorType> _doors;
    protected Room _previousRoom;
    protected Room[] _nextRooms;
    protected DoorType _entranceDoor;
    protected int _allowedNumOfDoors;
    protected bool isPlayerInside;
    protected bool isRoomCleared = false;
    [SerializeField] protected Excavator _excavator;
    [SerializeField] protected List<ChaserSpawn> ChaserSpawners = new List<ChaserSpawn>();
    public List<DoorParent> DoorInstances = new List<DoorParent>();
        
    // Public Properties
    public RoomTypes RoomType => _roomType;
    public Room PreviousRoom => _previousRoom;
    public int NumOfDoors => _numOfDoors;
    public List<DoorType> Doors => _doors;
    public Room[] NextRooms => _nextRooms;
    public int AllowedNumOfDoors => _allowedNumOfDoors;
    public DoorType EntranceDoor => _entranceDoor;

    // Public Setters
    public void SetRoomType(RoomTypes roomType)
    {
        Debug.Log("Attempting to set room type");
        _roomType = roomType;
    }

    void Start()
    {
        isPlayerInside = false;
    }
    public void SetNextRooms(Room[] nextRooms) => _nextRooms = nextRooms;
    public void SetPreviousRoom(Room previousRoom) => _previousRoom = previousRoom;
    public void setEntranceDoor(DoorType door) => _entranceDoor = door;
    public void setAllowedNumOfDoors(int numOfDoors) => _allowedNumOfDoors = numOfDoors;
    public void SetDoors(List<DoorType> d) => _doors = d;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerInside)
        {
            isPlayerInside = true;
            ActivateRoom();
            Debug.Log("Player entered");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            DeactivateRoom();
            Debug.Log("Player exited");
        }
    }

    public void lockDoors()
    {
        Debug.Log("Locking doors");
        foreach (var door in DoorInstances)
        {
            Debug.Log(door);
            door.Lock();
        }
    }

    public void unlockDoors()
    {
        Debug.Log("Unlocking doors");
        foreach (var door in DoorInstances)
        {
            door.Unlock();
            isRoomCleared = true;
        }

        if (_excavator != null)
        {
            _excavator.dropReward();
            Destroy(_excavator.gameObject);
        }
    }
    
    void ActivateRoom()
    {
        Debug.Log("Activating room: " + _roomType + ", Chaser Spawners in room: " + ChaserSpawners.Count);
        if (ChaserSpawners.Count > 0 && !isRoomCleared)
        {
            foreach (var spawner in ChaserSpawners)
            {
                Debug.Log(spawner);
                spawner.Activate(this);
                lockDoors();
            }
            Debug.Log("Activating Room");
        }
        else Debug.Log("No Chaser Spawners");

        if (_excavator != null)
        {
            _excavator.showCanvas();
        }
    }

    void DeactivateRoom()
    {
        if (ChaserSpawners.Count > 0)
        {
            foreach (var spawner in ChaserSpawners)
            {
                spawner.Deactivate();
            }

            Debug.Log("Deactivating Room");
        }
    }
    
    public void TryUnlockDoors()
    {
        Debug.Log("Trying to unlock doors...");
        if (ChaserSpawners.All(s => s.IsFinishedSpawning() && s.AreAllEnemiesDead()))
        {
            Debug.Log("Unlocked");
            unlockDoors();
        }
        else Debug.Log("Cant unlock doors");
    }
    

}
