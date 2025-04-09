using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Camera MainCamera { get; private set; }
    public RoomGenerator RoomGenerator { get; private set; }
    public Room.DoorType EntranceDoor { get; set; }
    [SerializeField] Shop[] shopPrefabs;
    public Shop[] shops;

    [SerializeField] private Transform[] shopLocations;
    private Shop shopInst;
    
    public LevelText lt { get; private set; }

    [SerializeField] private TextMeshProUGUI gameOverLevelText;
    
    private bool inBossRoom = false;

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
        spawnShops();
    }

    public void setLevelText(string text)
    {
        lt.SetText(text);
    }
    public bool InBossRoom
    {
        get => inBossRoom;
        set => inBossRoom = value;
    }

    private void spawnShops()
    {
        Debug.Log("Attempting to spawn shops");
        if (shopPrefabs.Length != shopLocations.Length)
        {
            Debug.LogWarning("Shop count mismatch.");
            return;
        }
        shops = new Shop[shopPrefabs.Length];
        for (int i = 0; i < shopPrefabs.Length; i++)
        {
            Debug.Log("Attempting to spawn shop " + shopPrefabs[i].name);
            shopInst = Instantiate(shopPrefabs[i], shopLocations[i].position, Quaternion.identity);
            shops[i] = shopInst.gameObject.GetComponent<Shop>();
            DontDestroyOnLoad(shopInst.gameObject);
        }
    }
    public void destroyCoinsInScene()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in coins)
        {
            GameObject.Destroy(coin);
        }
    }
    
}
