using System.Collections.Generic;
using UnityEngine;

public class EntryRoom : Room 
{
    void Start()
    {
        _roomType = RoomTypes.Entry;
        _entranceDoor = DoorType.Bottom;
        _allowedNumOfDoors = 4;
    }
    
}