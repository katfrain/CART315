using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private int initialNumRooms = 25;  // Number of rooms to generate
    [SerializeField] private Room[] roomPrefabs = new Room[5];  // Room prefabs based on difficulty
    [SerializeField] private Door doorPrefab;
    [SerializeField] private float height = 1;
    
    private int numRooms;
    private Room _entryRoom;
    private List<Room> _generatedRooms = new List<Room>(); // Keeps track of generated rooms
    private Queue<Room> _roomQueue = new Queue<Room>(); // Queue for breadth-first generation
    private float width;
    
    void Start()
    {
        width = height * 2;
        numRooms = initialNumRooms;
        _entryRoom = Instantiate(roomPrefabs[0], Vector3.zero, Quaternion.identity);
        _entryRoom.SetRoomType(Room.RoomTypes.Entry);
        _entryRoom.setEntranceDoor(Room.DoorType.Bottom);
        _entryRoom.transform.localScale = new Vector3(width, height, 0);
        _generatedRooms.Add(_entryRoom);
        _roomQueue.Enqueue(_entryRoom);  // Add entry room to the queue
        numRooms--;  // Reduce room count

        // Begin breadth-first room generation
        GenerateRooms();
    }

    private void GenerateRooms()
    {
        Debug.Log("Starting room generation");
        while (_roomQueue.Count > 0 && numRooms > 0)
        {
            Room currentRoom = _roomQueue.Dequeue();  // Get the next room from the queue
            Debug.Log("Current room: " + (initialNumRooms - numRooms) + " / " + currentRoom.RoomType );

            // Set allowed number of doors before generating new doors
            AdjustAllowedDoors(currentRoom);
            Debug.Log("Allowed doors " + currentRoom.AllowedNumOfDoors);

            // Generate doors based on the allowed number of doors for the current room
            List<Room.DoorType> availableDoors = GenerateDoors(currentRoom);
            currentRoom.SetDoors(availableDoors);
            Debug.Log("Generated Doors: " + string.Join(", ", currentRoom.Doors));

            int doorsGenerated = 0;

            foreach (Room.DoorType door in availableDoors)
            {
                Debug.Log("Generating Door: " + door.ToString());
                // Determine new position based on door
                Vector2 newPosition = Vector2.zero;
                Room.DoorType oppositeDoor = Room.DoorType.Left; // Default opposite door
                Vector3 doorPos = currentRoom.transform.position;
                float doorRotation = 0;

                switch (door)
                {
                    case Room.DoorType.Left:
                        newPosition = (Vector2)currentRoom.transform.position + Vector2.left * width;
                        doorPos.x -= width / 2;
                        oppositeDoor = Room.DoorType.Right;
                        break;
                    case Room.DoorType.Right:
                        newPosition = (Vector2)currentRoom.transform.position + Vector2.right * width;
                        doorPos.x += width / 2;
                        oppositeDoor = Room.DoorType.Left;
                        break;
                    case Room.DoorType.Top:
                        newPosition = (Vector2)currentRoom.transform.position + Vector2.up * height;
                        doorPos.y += height / 2;
                        doorRotation = 90;
                        oppositeDoor = Room.DoorType.Bottom;
                        break;
                    case Room.DoorType.Bottom:
                        newPosition = (Vector2)currentRoom.transform.position + Vector2.down * height;
                        doorPos.y -= height / 2;
                        doorRotation = 90;
                        oppositeDoor = Room.DoorType.Top;
                        break;
                }
                Debug.Log("Position: " + newPosition.ToString());
                // Check if the new position is valid (within the grid bounds and not already occupied)
                if (IsPositionValid(newPosition))
                {
                    Debug.Log("Position is valid: " + newPosition.ToString());
                    // Instantiate new room at the valid position
                    int temp = (int)currentRoom.RoomType + 2;
                    if (temp >= roomPrefabs.Length) temp = roomPrefabs.Length - 1;
                    int difficulty = Random.Range(1, temp); // Random difficulty level for the new room
                    if (numRooms == 2) difficulty = 4;
                    Room newRoom = Instantiate(roomPrefabs[difficulty], newPosition, Quaternion.identity);
                    newRoom.transform.localScale = new Vector3(width, height, 0);
                    Room.RoomTypes roomType = (Room.RoomTypes)difficulty + 1;
                    newRoom.SetRoomType(roomType);
                    _generatedRooms.Add(newRoom);
                    numRooms--;

                    // Set the new room's previous room and door connection
                    newRoom.SetPreviousRoom(currentRoom);
                    newRoom.setEntranceDoor(oppositeDoor);
                    
                    // instantiate doors
                    Door newDoor = Instantiate(doorPrefab, doorPos, Quaternion.identity);
                    newDoor.Direction = doorRotation;
                    newDoor.Width = (float)(width * 0.1);
                    newDoor.Height = (float)(height * 0.4);

                    // Enqueue the new room for further processing
                    _roomQueue.Enqueue(newRoom);
                    Debug.Log("Enqueued New Room");
                }
                else Debug.Log("Room Creation Failed");

                doorsGenerated++; // Increment the number of doors generated
            }
        }
    }

    private List<Room.DoorType> GenerateDoors(Room room)
    {
        List<Room.DoorType> returnDoors = new List<Room.DoorType> { };
        if (room.AllowedNumOfDoors > 1)
        {
            List<Room.DoorType> possibleDoors = new List<Room.DoorType> { Room.DoorType.Left, Room.DoorType.Right, Room.DoorType.Top, Room.DoorType.Bottom };

            // Remove entrance door from possible selections
            possibleDoors.Remove(room.EntranceDoor);

            System.Random rand = new System.Random();

            // Determine number of additional doors (1 to allowed number of doors)
            int numAdditionalDoors = rand.Next(1, room.AllowedNumOfDoors);

            // Shuffle possible doors
            for (int i = possibleDoors.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (possibleDoors[i], possibleDoors[j]) = (possibleDoors[j], possibleDoors[i]); // Swap elements
            }

            // Select 'numAdditionalDoors' elements from shuffled list
            returnDoors.AddRange(possibleDoors.GetRange(0, numAdditionalDoors));
        }

        return returnDoors;
    }

    private void AdjustAllowedDoors(Room room)
    {
        // Adjust allowed number of doors based on how many rooms are left
        if (numRooms <= 0)
        {
            room.setAllowedNumOfDoors(0);  // No more doors for the last room
        }
        else if (numRooms == 1)
        {
            room.setAllowedNumOfDoors(1);
        }
        else if (numRooms == 2)
        {
            room.setAllowedNumOfDoors(2);
        }
        else
        {
            room.setAllowedNumOfDoors(4);  // Most rooms can have up to 4 doors
        }
    }

    private bool IsPositionValid(Vector2 position)
    {
        // Check if the position is within grid bounds and is not occupied by another room
        foreach (Room room in _generatedRooms)
        {
            if (room.transform.position == (Vector3)position)
                return false; // Position already occupied
        }
        return true;
    }
}
