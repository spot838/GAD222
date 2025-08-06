using System.Linq;
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

    public StoryBlock[] Act1StoryBlocks;
    public StoryBlock[] Act2StoryBlocks;
    public StoryBlock[] Act3StoryBlocks;
    public StoryBlock[] PrologueStoryBlocks;
    public void OptionSelected(int value)
    {
        if (value == 1)
        {
            LoadCurrentStoryBlock(ChangeCurrentStoryBlock(currentStoryBlock.option1StoryBlock));
        }
        else if (value == 2)
        {
            LoadCurrentStoryBlock(ChangeCurrentStoryBlock(currentStoryBlock.option2StoryBlock));
        }
        else if (value == 3)
        {
            LoadCurrentStoryBlock(ChangeCurrentStoryBlock(currentStoryBlock.option4StoryBlock));
        }
        else if (value == 4)
        {
            LoadCurrentStoryBlock(ChangeCurrentStoryBlock(currentStoryBlock.option4StoryBlock));
        }
        else
        {
            TextManager.Instance.AddTextPlain("There has been an error with the selected option");
        }
    }
    public bool ChangeCurrentStoryBlock(StoryBlock newStoryBlock)
    {
        bool didChange = false;
        if (newStoryBlock != null && newStoryBlock != currentStoryBlock)
        {
            currentStoryBlock = newStoryBlock;
            didChange = true;
            return didChange;
        }
        return didChange;
    }
    void LoadCurrentStoryBlock(bool shouldLoad)
    {
        #region Title Change Check
        if (Act1StoryBlocks.Contains(currentStoryBlock))
        {
            TextManager.Instance.titleTextDisplay.text = "Act 1: The Village";
            BackgroundImageManager.Instance.LoadBackgroundImage(1);
        }
        else if (Act2StoryBlocks.Contains(currentStoryBlock))
        {
            TextManager.Instance.titleTextDisplay.text = "Act 2: The Riots";
            BackgroundImageManager.Instance.LoadBackgroundImage(2);
        }
        else if (Act3StoryBlocks.Contains(currentStoryBlock))
        {
            TextManager.Instance.titleTextDisplay.text = "Act 3: The Renewal";
            BackgroundImageManager.Instance.LoadBackgroundImage(3);
        }
        else if (PrologueStoryBlocks.Contains(currentStoryBlock))
        {
            TextManager.Instance.titleTextDisplay.text = "The Prologue";
            BackgroundImageManager.Instance.LoadBackgroundImage(4);
        }
        #endregion
        if (shouldLoad)
        {
            TextManager.Instance.AddTextNewLine(currentStoryBlock.mainText);
            TextManager.Instance.ConstructOptionsTextButtons(currentStoryBlock);
        }
    }

    private void Awake()
    {
        CreateSingleton();
    }
    private void Start()
    {
        TextManager.Instance.ClearMainText();
        TextManager.Instance.AddTextPlain(currentStoryBlock.mainText);
        TextManager.Instance.ConstructOptionsTextButtons(currentStoryBlock);

        BackgroundImageManager.Instance.LoadBackgroundImage(0);
    }
}