using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Camera MainCamera { get; private set; }
    public RoomGenerator RoomGenerator { get; private set; }
    
    public LevelText lt { get; private set; }

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
        RoomGenerator = FindFirstObjectByType<RoomGenerator>();
        lt = FindFirstObjectByType<LevelText>();

        if (RoomGenerator == null)
            Debug.LogWarning("RoomGenerator could not be found in the scene.");
    }

    private void Start()
    {
        setLevelText("0");
    }

    public void setLevelText(string text)
    {
        lt.SetText(text);
    }
}
