using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _levelText;
    private bool _enabled = false;
    
    public static GameOverScreen Instance;
    
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
    }
    void Start()
    {
        _enabled = false;
        updateEnabled();
    }

    public bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            updateEnabled();
        }
    }

    public void updateLevelText(int level)
    {
        _levelText.text = "You Reached Level " + level.ToString();
    }

    private void updateEnabled()
    {
        _canvas.enabled = _enabled;
    }
}
