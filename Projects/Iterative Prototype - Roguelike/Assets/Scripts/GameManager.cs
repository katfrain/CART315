using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Camera MainCamera { get; private set; }
    public RoomGenerator RoomGenerator { get; private set; }
    public Room.DoorType EntranceDoor { get; set; }
    
    public LevelText lt { get; private set; }

    [SerializeField] private TextMeshProUGUI gameOverLevelText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        MainCamera = Camera.main;
        lt = FindFirstObjectByType<LevelText>();
        if (FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            DontDestroyOnLoad(eventSystem);
        }
        
    }

    private void Start()
    {
        RoomGenerator = RoomGenerator.Instance;
        if (RoomGenerator == null)
            Debug.LogWarning("RoomGenerator could not be found in the scene.");
        setLevelText("0");
    }

    public void setLevelText(string text)
    {
        lt.SetText(text);
    }
    
}
