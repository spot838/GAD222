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
    public TextMeshProUGUI optionsTextDisplay;
    [SerializeField] [TextArea] string debugText;

    public void ClearMainText()
    {
        Debug.Log("Called ClearMainText");
        mainTextDisplay.text = string.Empty;
    }
    public void AddTextPlain(string newText)
    {
        Debug.Log("Called AddTextPlain");
        mainTextDisplay.text += newText;
    }
    public void AddTextNewLine(string newText)
    {
        Debug.Log("Called AddTextNewLine");
        mainTextDisplay.text += "\n"+newText;
    }
    public void AddTextDebug()
    {
        Debug.Log("Called AddTextDebug");
        debugText = debugText.Replace("\n", "\n");
        mainTextDisplay.text += debugText;
    }

    public void ConstructOptionsTextDisplay(StoryBlock storyBlock)
    {
        Debug.Log("Called ConstructOptionsTextDisplay");
        optionsTextDisplay.text = ("Option 1: " + storyBlock.option1Text) + ("\nOption 2: " + storyBlock.option2Text) + ("\nOption 3: " + storyBlock.option3Text) + ("\nOption 4: " + storyBlock.option4Text);
        GameManager.Instance.listeningForInputs = true;
    }

    private void Awake()
    {
        CreateSingleton();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D Pressed");
            AddTextDebug();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C Pressed");
            ClearMainText();
        }
    }
}