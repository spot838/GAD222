using UnityEngine;
using UnityEngine.UI;
public class BackgroundImageManager : MonoBehaviour
{
    public static BackgroundImageManager Instance;
    void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }
    public bool isFlipped = false;
    [Header("Background Image & Rect")]
    public Image backgroundImage;
    public RectTransform backgroundImageRectTransform;
    [Space(10)]
    public Image mirroredImage;
    public RectTransform mirroredImageRectTransform;

    [Header("Per Act Images & Rects")]
    public Sprite tutorialImage;
    public Vector2 tutorialPos;
    public Vector2 tutorialSize;
    [Space(5)]
    public Vector2 tutorialMirrorPos;
    public Vector2 tutorialMirrorSize;

    [Space(20)]
    public Sprite act1Image;
    public Vector2 act1Pos;
    public Vector2 act1Size;
    [Space(5)]
    public Vector2 act1MirrorPos;
    public Vector2 act1MirrorSize;

    [Space(20)]
    public Sprite act2Image;
    public Vector2 act2Pos;
    public Vector2 act2Size;
    [Space(5)]
    public Vector2 act2MirrorPos;
    public Vector2 act2MirrorSize;

    [Space(20)]
    public Sprite act3Image;
    public Vector2 act3Pos;
    public Vector2 act3Size;
    [Space(5)]
    public Vector2 act3MirrorPos;
    public Vector2 act3MirrorSize;

    [Space(20)]
    public Sprite epilogueImage;
    public Vector2 epiloguePos;
    public Vector2 epilogueSize;
    [Space(5)]
    public Vector2 epilogueMirrorPos;
    public Vector2 epilogueMirrorSize;


    public void LoadBackgroundImage(int actInt)
    {
        actInt = Mathf.Clamp(actInt, 0, 4);

        backgroundImage.gameObject.SetActive(true);
        mirroredImage.gameObject.SetActive(true);

        if (actInt == 0)
        {
            backgroundImage.rectTransform.localScale = new Vector3(-1, 1, 1);

            backgroundImage.sprite = tutorialImage;

            backgroundImageRectTransform.anchoredPosition = tutorialPos;
            backgroundImageRectTransform.sizeDelta = tutorialSize;


            
            mirroredImage.sprite = tutorialImage;

            mirroredImageRectTransform.anchoredPosition = tutorialMirrorPos;
            mirroredImageRectTransform.sizeDelta = tutorialMirrorSize;
        }
        else if (actInt == 1)
        {
            backgroundImage.rectTransform.localScale = new Vector3(-1, 1, 1);

            backgroundImage.sprite = act1Image;

            backgroundImageRectTransform.anchoredPosition = act1Pos;
            backgroundImageRectTransform.sizeDelta = act1Size;



            mirroredImage.sprite = act1Image;

            mirroredImageRectTransform.anchoredPosition = act1MirrorPos;
            mirroredImageRectTransform.sizeDelta = act1MirrorSize;
        }
        else if (actInt == 2)
        {
            backgroundImage.rectTransform.localScale = new Vector3(1, 1, 1);

            backgroundImage.sprite = act2Image;

            backgroundImageRectTransform.anchoredPosition = act2Pos;
            backgroundImageRectTransform.sizeDelta = act2Size;



            mirroredImage.sprite = act2Image;

            mirroredImageRectTransform.anchoredPosition = act2MirrorPos;
            mirroredImageRectTransform.sizeDelta = act2MirrorSize;
        }
        else if (actInt == 3)
        {
            backgroundImage.rectTransform.localScale = new Vector3(-1, 1, 1);

            backgroundImage.sprite = act3Image;

            backgroundImageRectTransform.anchoredPosition = act3Pos;
            backgroundImageRectTransform.sizeDelta = act3Size;



            mirroredImage.sprite = act3Image;

            mirroredImageRectTransform.anchoredPosition = act3MirrorPos;
            mirroredImageRectTransform.sizeDelta = act3MirrorSize;
        }
        else if (actInt == 4)
        {
            backgroundImage.rectTransform.localScale = new Vector3(-1, 1, 1);

            backgroundImage.sprite = epilogueImage;

            backgroundImageRectTransform.anchoredPosition = epiloguePos;
            backgroundImageRectTransform.sizeDelta = epilogueSize;



            mirroredImage.sprite = epilogueImage;

            mirroredImageRectTransform.anchoredPosition = epilogueMirrorPos;
            mirroredImageRectTransform.sizeDelta = epilogueMirrorSize;
        }
    }



    private void Awake()
    {
        CreateSingleton();
    }
}