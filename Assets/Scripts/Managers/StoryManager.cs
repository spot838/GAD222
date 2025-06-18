using UnityEngine;
public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;
    void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    public StoryBlock currentStoryBlock;
    public void OptionSelected(int value)
    {
        if (value == 1)
        {
            ChangeCurrentStoryBlock(currentStoryBlock.option1StoryBlock);
        }
        else if (value == 2)
        {
            ChangeCurrentStoryBlock(currentStoryBlock.option2StoryBlock);
        }
        else if (value == 3)
        {
            ChangeCurrentStoryBlock(currentStoryBlock.option3StoryBlock);
        }
        else if (value == 4)
        {
            ChangeCurrentStoryBlock(currentStoryBlock.option4StoryBlock);
        }
        else
        {
            TextManager.Instance.AddTextPlain("There has been an error with the selected option");
        }
        LoadCurrentStoryBlock();
    }
    public void ChangeCurrentStoryBlock(StoryBlock newStoryBlock)
    {
        currentStoryBlock = newStoryBlock;
    }
    void LoadCurrentStoryBlock()
    {
        TextManager.Instance.AddTextNewLine(currentStoryBlock.mainText);
        TextManager.Instance.ConstructOptionsTextDisplay(currentStoryBlock);
    }

    private void Awake()
    {
        CreateSingleton();
    }
    private void Start()
    {
        TextManager.Instance.ClearMainText();
        TextManager.Instance.AddTextPlain(currentStoryBlock.mainText);
        TextManager.Instance.ConstructOptionsTextDisplay(currentStoryBlock);
    }
}