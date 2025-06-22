using UnityEngine;
[CreateAssetMenu (menuName = "Story Block")]
public class StoryBlock : ScriptableObject
{
    //public string storyBlockName;
    [TextArea] public string mainText;
    [Space(20)]
    [Header("Options")]
    [TextArea] public string option1Text = "New Text";
    public StoryBlock option1StoryBlock;
    [TextArea] public string option2Text = "New Text";
    public StoryBlock option2StoryBlock;
    [TextArea] public string option3Text = "New Text";
    public StoryBlock option3StoryBlock;
    [TextArea] public string option4Text = "New Text";
    public StoryBlock option4StoryBlock;
    //[Space(50)]
    //[Header("Linked StoryBlocks")]
}