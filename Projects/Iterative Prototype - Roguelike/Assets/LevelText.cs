using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetText(string text)
    {
        _text.SetText("Level: " + text);
    }
    
}
