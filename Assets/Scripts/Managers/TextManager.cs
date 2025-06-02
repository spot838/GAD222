using TMPro;
using UnityEngine;
public class TextManager : MonoBehaviour
{
    public static TextManager Instance;
    void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }
    public TextMeshProUGUI mainTextDisplay;
    private void Awake()
    {
        CreateSingleton();
    }
    public void ClearMainText()
    {
        mainTextDisplay.text = string.Empty;
    }
    public void AddText(string newText)
    {
        mainTextDisplay.text += "/n"+newText;
    }
}