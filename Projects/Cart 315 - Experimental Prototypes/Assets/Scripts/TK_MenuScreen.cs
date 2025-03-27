using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TK_MenuScreen : MonoBehaviour
{
    public TextMeshProUGUI GameTitle;
    public TextMeshProUGUI GameOverText;
    public Button StartButton;
    public Button ExitButton;
    public TextMeshProUGUI StartButtonText;
    public TextMeshProUGUI ExitButtonText;
    public static bool GameOver = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartButtonText.text = "START GAME";
        GameOverText.gameObject.SetActive(false);
        TK_Enemy.score = 0;
        StartButton.onClick.AddListener(() => { startGame(); });
        ExitButton.onClick.AddListener(() => { exitGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
        {
            GameOverText.gameObject.SetActive(true);
            StartButtonText.text = "RESTART";
        }

        if (!GameOver)
        {
            GameOverText.gameObject.SetActive(false);
            StartButtonText.text = "START GAME";
        }
        
    }

    private void startGame()
    {
        SceneManager.LoadScene(3);
    }

    private void exitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
