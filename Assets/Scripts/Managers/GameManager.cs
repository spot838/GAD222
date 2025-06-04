using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    public bool listeningForInputs = false;

    private void Awake()
    {
        CreateSingleton();
    }
    private void Update()
    {
        if (listeningForInputs)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("1 Pressed");
                // Call Option 1
                TextManager.Instance.ConstructOptionsTextDisplay(StoryManager.Instance.currentStoryBlock.option1StoryBlock);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("2 Pressed");
                // Call Option 2
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("3 Pressed");
                // Call Option 3
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("4 Pressed");
                // Call Option 4
            }
        }
    }
}