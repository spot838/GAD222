using UnityEngine;
[CreateAssetMenu (menuName = "Story Block")]
public class StoryBlock : ScriptableObject
{
    //public string storyBlockName;
    [TextArea] public string mainText;
    [Space(20)]
    [Header("Options")]
    [TextArea] public string option1Text = "New Text";
    [TextArea] public string option2Text = "New Text";
    [TextArea] public string option3Text = "New Text";
    [TextArea] public string option4Text = "New Text";
    [Space(50)]
    [Header("Linked StoryBlocks")]
    public StoryBlock option1StoryBlock;
    public StoryBlock option2StoryBlock;
    public StoryBlock option3StoryBlock;
    public StoryBlock option4StoryBlock;
}