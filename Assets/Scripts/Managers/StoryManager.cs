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

    public StoryBlock[] storyBlocks = new StoryBlock[0];
    public StoryBlock currentStoryBlock = null;
    public int storyCount = -1;
    private void Awake()
    {
        CreateSingleton();
    }
    private void Start()
    {
        //TextManager.Instance.AddTextPlain();
    }
}