using System.Collections.Generic;
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
    public void SetNextRooms(Room[] nextRooms) => _nextRooms = nextRooms;
    public void SetPreviousRoom(Room previousRoom) => _previousRoom = previousRoom;
    public void setEntranceDoor(DoorType door) => _entranceDoor = door;
    public void setAllowedNumOfDoors(int numOfDoors) => _allowedNumOfDoors = numOfDoors;
    public void SetDoors(List<DoorType> d) => _doors = d;
    

}
