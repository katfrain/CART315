using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private int initialNumRooms = 25;  // Number of rooms to generate
    [SerializeField] private Room[] roomPrefabs = new Room[5];  // Room prefabs based on difficulty
    [SerializeField] private Door doorPrefab;
    [SerializeField] private DoorLevelSpawner dlsPrefab;
    [SerializeField] private LeaveDungeonDoor lddPrefab;
    [SerializeField] private float _height = 1;
    [SerializeField] private Room startRoom;
    [SerializeField] private DoorLevelSpawner initialSpawnDoor;
    
    private int numRooms;
    private Room _entryRoom;
    private List<Room> _generatedRooms = new List<Room>(); // Keeps track of generated rooms
    private Queue<Room> _roomQueue = new Queue<Room>(); // Queue for breadth-first generation
    private List<DoorParent> _generatedDoors = new List<DoorParent>();
    private float _width;
    private Camera camera;
    private Player player;
    
    private int _levelCount = 0;
    
    public static RoomGenerator Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = Player.Instance;
        _levelCount = 0;
        _width = _height * 2;
        numRooms = initialNumRooms;
        camera = GameManager.Instance.MainCamera;
        DontDestroyOnLoad(camera.gameObject);
        _generatedRooms.Add(startRoom);
        DontDestroyOnLoad(startRoom.gameObject); 
        _generatedDoors.Add(initialSpawnDoor);
        DontDestroyOnLoad(initialSpawnDoor.gameObject); 
    }

    public float height
    {
        get { return _height; }
    }

    public float width
    {
        get { return _width; }
    }
    
    public int levelCount { get { return _levelCount; } set { _levelCount = value; } }
    public void GenerateNewLevel(Room.DoorType entranceDoor)
    {
        int dungeonSceneIndex = 2; 
        GameManager.Instance.EntranceDoor = entranceDoor;
        // Check if already in the dungeon scene
        if (SceneManager.GetActiveScene().buildIndex != dungeonSceneIndex)
        {
            SceneLoader.Instance.LoadNewScene(dungeonSceneIndex, () =>
            {
                StartRoomGeneration(GameManager.Instance.EntranceDoor);
                Shop.Instance.Visible = false;
            });
        }
        else
        {
            // If already in the dungeon scene, generate rooms directly
        StartRoomGeneration(GameManager.Instance.EntranceDoor);
        }
    }

    private void StartRoomGeneration(Room.DoorType entranceDoor)
    {
        Debug.Log("Starting Room Generator at level " + _levelCount);
        Vector3 entryRoomPos = new Vector3(0, 0, 0);

        // Clear previous level only if it's not the first level
        if (_levelCount > 0)
        {
            clearRooms();
            Debug.Log("Cleared previous rooms, doors, and spawners.");
            entryRoomPos = Vector3.zero;
            player.transform.position = Vector3.zero;
            camera.transform.position = new Vector3(0, 0, -10);
        }
        else
        {
            entryRoomPos = Vector3.zero + new Vector3(0, height);
            switch (entranceDoor)
            {
                case Room.DoorType.Top:
                    player.transform.position = new Vector3(0, (height + height / 10), 0);
                    break;
                case Room.DoorType.Left:
                    player.transform.position = new Vector3(width + width / 20, 0, 0);
                    break;
                case Room.DoorType.Right:
                    player.transform.position = new Vector3(width - width / 20, 0, 0);
                    break;
                case Room.DoorType.Bottom:
                    player.transform.position = new Vector3(0, (height - height / 10), 0);
                    break;
            }
        }
        // Instantiate the entry room
        _entryRoom = Instantiate(roomPrefabs[0], entryRoomPos, Quaternion.identity);
        _entryRoom.transform.localScale = new Vector3(width, height, 0);

        // Set room type and doors
        _entryRoom.SetRoomType(Room.RoomTypes.Entry);
        _entryRoom.setEntranceDoor(entranceDoor);

        // Track entry room
        _generatedRooms.Add(_entryRoom);
        _roomQueue.Enqueue(_entryRoom);

        // Begin room generation
        numRooms = initialNumRooms;
        GenerateRooms();

        // Increment level count for next level
        _levelCount++;
        GameManager.Instance.setLevelText((_levelCount).ToString());
    }
    public void clearRooms()
    {
        // Destroy previously generated rooms, doors, and spawners
        foreach (Room room in _generatedRooms)
        {
            Destroy(room.gameObject);
        }

        foreach (Room room in _roomQueue)
        {
            Destroy(room.gameObject);
        }

        foreach (DoorParent door in _generatedDoors)
        {
            Destroy(door.gameObject);
        }

        // Clear the lists and queues for the next level
        _roomQueue.Clear();
        _generatedRooms.Clear();
        _generatedDoors.Clear();

        // Destroy entry room if exists
        if (_entryRoom != null)
        {
            Destroy(_entryRoom.gameObject);
            _entryRoom = null;
        }
    }

    private void GenerateRooms()
    {
        Debug.Log("Starting room generation");
        while (_roomQueue.Count > 0 && numRooms > 0)
        {
            Room currentRoom = _roomQueue.Dequeue();

            // Adjust the room's door generation based on its allowed number of doors
            AdjustAllowedDoors(currentRoom);

            // Generate and place doors for the room
            List<Room.DoorType> availableDoors = GenerateDoors(currentRoom);
            currentRoom.SetDoors(availableDoors);

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
                        newPosition = (Vector2)currentRoom.transform.position + Vector2.left * _width;
                        doorPos.x -= _width / 2;
                        oppositeDoor = Room.DoorType.Right;
                        break;
                    case Room.DoorType.Right:
                        newPosition = (Vector2)currentRoom.transform.position + Vector2.right * _width;
                        doorPos.x += _width / 2;
                        doorRotation = 180;
                        oppositeDoor = Room.DoorType.Left;
                        break;
                    case Room.DoorType.Top:
                        newPosition = (Vector2)currentRoom.transform.position + Vector2.up * _height;
                        doorPos.y += _height / 2;
                        doorRotation = 90;
                        oppositeDoor = Room.DoorType.Bottom;
                        break;
                    case Room.DoorType.Bottom:
                        newPosition = (Vector2)currentRoom.transform.position + Vector2.down * _height;
                        doorPos.y -= _height / 2;
                        doorRotation = 270;
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
                    if (numRooms == 2)
                    {
                        difficulty = 4;
                        createDoorToNextLevel(newPosition, door);
                    }
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
                    newDoor.Width = (float)(_width * 0.1);
                    newDoor.Height = (float)(_height * 0.4);
                    _generatedDoors.Add(newDoor);

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
        Debug.Log("Entrance Door: "+room.EntranceDoor.ToString());
        List<Room.DoorType> returnDoors = new List<Room.DoorType> { };
        if (room.AllowedNumOfDoors > 1)
        {
            List<Room.DoorType> possibleDoors = new List<Room.DoorType> { Room.DoorType.Left, Room.DoorType.Right, Room.DoorType.Top, Room.DoorType.Bottom };

            // Remove entrance door from possible selections
            possibleDoors.Remove(room.EntranceDoor);
            Debug.Log("Possible Doors: ");
            foreach (Room.DoorType door in possibleDoors)
            {
                Debug.Log(door.ToString());
            }

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

    private void createDoorToNextLevel(Vector3 position, Room.DoorType door)
    {
        float doorPosx = position.x;
        float doorPosy = position.y;
        float lddoorPosx = position.x;
        float lddoorPosy = position.y;
        float lddoorRot = 0;
        float doorRot = 0;

        // Handle door positioning and rotation based on direction
        switch (door)
        {
            case Room.DoorType.Left:
                doorPosx -= _width / 2;
                doorRot = 180;
                lddoorPosy -= _height / 2;
                lddoorRot = 270;
                break;
            case Room.DoorType.Right:
                doorPosx += _width / 2;
                lddoorPosy -= _height / 2;
                lddoorRot = 270;
                break;
            case Room.DoorType.Top:
                doorPosy += _height / 2;
                doorRot = 90;
                lddoorPosx += _width / 2;
                break;
            case Room.DoorType.Bottom:
                doorPosy -= _height / 2;
                doorRot = 270;
                lddoorPosx += _width / 2;
                break;
        }

        // Instantiate a DoorLevelSpawner that will trigger the next level generation
        DoorLevelSpawner dls = Instantiate(dlsPrefab, new Vector3(doorPosx, doorPosy, 0), Quaternion.identity);
        dls.Direction = doorRot;
        dls.Width = (float)(_width * 0.1);
        dls.Height = (float)(_height * 0.4);
        _generatedDoors.Add(dls);
        
        LeaveDungeonDoor ldd = Instantiate(lddPrefab, new Vector3(lddoorPosx, lddoorPosy, 0), Quaternion.identity);
        ldd.Direction = lddoorRot;
        ldd.Width = (float)(_width * 0.1);
        ldd.Height = (float)(_height * 0.4);
        _generatedDoors.Add(ldd);
    }
}
