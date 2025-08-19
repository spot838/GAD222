using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//[RequireComponent(typeof(TMP_Text))]
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
    public TextMeshProUGUI titleTextDisplay;
    public TMP_Text mainTextDisplay;
    public TextMeshProUGUI optionsTextDisplay;
    [SerializeField] GameObject optionsButton1;
    [SerializeField] TextMeshProUGUI optionsText1;
    [SerializeField] GameObject optionsButton2;
    [SerializeField] TextMeshProUGUI optionsText2;
    [SerializeField] GameObject optionsButton3;
    [SerializeField] TextMeshProUGUI optionsText3;
    [SerializeField] GameObject optionsButton4;
    [SerializeField] TextMeshProUGUI optionsText4;

    [SerializeField] StoryBlock notYetImplementedBlock;

    [SerializeField] [TextArea] string debugText;

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] RectTransform viewport;
    [SerializeField] RectTransform content;

    [SerializeField] TextScrollController textScrollController;

    int _visibleCharacterIndex;
    Coroutine _typewriterCoroutine;
    WaitForSeconds _simpleDelay;
    WaitForSeconds _interpunctuationDelay;
    [Header("Font Settings")]
    public bool useDyslexicFont = false;
    public Toggle dyslexicFontToggle;
    public TMP_FontAsset defaultFont;
    public TMP_FontAsset dyslexicFont;
    [Space(15)]
    public bool useBlackFont = false;
    public Toggle fontColourToggle;
    public Color fontWhite;
    public Color fontBlack;

    [Header("Typewriter Settings")]
    [SerializeField] float charactersPerSecond = 20f;
    [SerializeField] float interpunctuationDelay = 0.5f;
    [field:SerializeField] public bool currentlySkipping { get; private set; }
    WaitForSeconds _skipDelay;
    [Header("Skip Options")]
    public bool quickSkip;
    [SerializeField] Toggle quickSkipToggle;
    [SerializeField][Min(1)] private int skipSpeedup = 5;
    [Header("Event Stuff")]
    WaitForSeconds _textboxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] float sendDoneDelay = 0.25f;
    public static event Action CompleteTextRevealed;
    public static event Action<char> CharacterRevealed;

    #region Add/Clear Main Text
    public void ClearMainText()
    {
        Debug.Log("Called ClearMainText");
        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }
        mainTextDisplay.text = string.Empty;
        _visibleCharacterIndex = 0;
    }
    public void SetText(string newText)
    {
        Debug.Log("Called SetText");

        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }

        mainTextDisplay.text = newText;

        mainTextDisplay.maxVisibleCharacters = 0;
        _visibleCharacterIndex = 0;

        _typewriterCoroutine = StartCoroutine(routine:Typewriter());
    }
    public void AddTextPlain(string newText)
    {
        Debug.Log("Called AddTextPlain");

        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }

        mainTextDisplay.text += newText;

        mainTextDisplay.ForceMeshUpdate();
        mainTextDisplay.maxVisibleCharacters = mainTextDisplay.textInfo.characterCount - newText.Length;
        //Debug.Log("Text Character Count: " + mainTextDisplay.textInfo.characterCount);
        //Debug.Log("Text Visible Characters: " + mainTextDisplay.maxVisibleCharacters);
        _visibleCharacterIndex = mainTextDisplay.maxVisibleCharacters;
        //Debug.Log("visibleCharIndex: " + _visibleCharacterIndex);

        _typewriterCoroutine = StartCoroutine(routine: Typewriter());
        ScrollToVisibleTextPosition();
    }
    public void AddTextNewLine(string newText)
    {
        Debug.Log("Called AddTextNewLine");
        AddTextPlain("\n"+"\n"+newText);
    }
    public void AddTextDebug()
    {
        Debug.Log("Called AddTextDebug");

        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }

        debugText = debugText.Replace("\n", "\n");
        mainTextDisplay.text += debugText;

        mainTextDisplay.ForceMeshUpdate();
        mainTextDisplay.maxVisibleCharacters = mainTextDisplay.textInfo.characterCount - debugText.Length;
        Debug.Log("Text Character Count: " + mainTextDisplay.textInfo.characterCount);
        Debug.Log("Text Visible Characters: " + mainTextDisplay.maxVisibleCharacters);
        _visibleCharacterIndex = mainTextDisplay.maxVisibleCharacters;
        Debug.Log("visibleCharIndex: " + _visibleCharacterIndex);

        _typewriterCoroutine = StartCoroutine(routine: Typewriter());
    }
    #endregion

    public void ConstructOptionsTextDisplay(StoryBlock storyBlock)
    {
        Debug.Log("Called ConstructOptionsTextDisplay");
        optionsTextDisplay.text = ("Option 1: " + storyBlock.option1Text) + ("\nOption 2: " + storyBlock.option2Text) + ("\nOption 3: " + storyBlock.option3Text) + ("\nOption 4: " + storyBlock.option4Text);
        GameManager.Instance.listeningForInputs = true;
    }
    public void ConstructOptionsTextButtons(StoryBlock storyBlock)
    {
        Debug.Log("Called ConstructOptionsTextButtons");
        optionsButton1.SetActive(false);
        optionsButton2.SetActive(false);
        optionsButton3.SetActive(false);
        optionsButton4.SetActive(false);
        if (storyBlock.option1StoryBlock != null)
        {
            optionsButton1.SetActive(true);
            optionsText1.text = storyBlock.option1Text;

            optionsButton1.GetComponent<Button>().onClick.RemoveAllListeners();
            optionsButton1.GetComponent<Button>().onClick.AddListener(() => StoryManager.Instance.OptionSelected(1));
        }
        if (storyBlock.option2StoryBlock != null)
        {
            optionsButton2.SetActive(true);
            optionsText2.text = storyBlock.option2Text;

            optionsButton2.GetComponent<Button>().onClick.RemoveAllListeners();
            optionsButton2.GetComponent<Button>().onClick.AddListener(() => StoryManager.Instance.OptionSelected(2));
        }
        if (storyBlock.option3StoryBlock != null)
        {
            optionsButton3.SetActive(true);
            optionsText3.text = storyBlock.option3Text;

            optionsButton3.GetComponent<Button>().onClick.RemoveAllListeners();
            optionsButton3.GetComponent<Button>().onClick.AddListener(() => StoryManager.Instance.OptionSelected(3));
        }
        if (storyBlock.option4StoryBlock != null)
        {
            optionsButton4.SetActive(true);
            optionsText4.text = storyBlock.option4Text;

            optionsButton4.GetComponent<Button>().onClick.RemoveAllListeners();
            optionsButton4.GetComponent<Button>().onClick.AddListener(() => StoryManager.Instance.OptionSelected(4));
        }

        if (storyBlock.option1StoryBlock == notYetImplementedBlock)
        {
            optionsButton1.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        if (storyBlock.option2StoryBlock == notYetImplementedBlock)
        {
            optionsButton2.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        if (storyBlock.option3StoryBlock == notYetImplementedBlock)
        {
            optionsButton3.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        if (storyBlock.option4StoryBlock == notYetImplementedBlock)
        {
            optionsButton4.GetComponent<Button>().onClick.RemoveAllListeners();
        }

        GameManager.Instance.listeningForInputs = true;
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = mainTextDisplay.textInfo;

        while (_visibleCharacterIndex < textInfo.characterCount + 1)
        {
            int LastCharacterIndex = textInfo.characterCount - 1;

            if (_visibleCharacterIndex == LastCharacterIndex)
            {
                mainTextDisplay.maxVisibleCharacters++;
                yield return _textboxFullEventDelay;
                CompleteTextRevealed?.Invoke();
                yield break;
            }

            char character = textInfo.characterInfo[_visibleCharacterIndex].character;
            mainTextDisplay.maxVisibleCharacters++;

            if (!currentlySkipping && character == '?' || character == '.' || character == ',' || character == ';' || character == '!' || character == '-' || character == '?') // last character would be : but we dont want to pause on that for this project
            {
                yield return interpunctuationDelay;
            }
            else
            {
                yield return currentlySkipping ? _skipDelay : _simpleDelay;
            }

            CharacterRevealed?.Invoke(character);
            _visibleCharacterIndex++;
            //textScrollController.UpdateScrollPosition();
        }
    }

    public void Skip()
    {
        if (currentlySkipping)
        {
            return;
        }
        currentlySkipping = true;

        if (!quickSkip)
        {
            StartCoroutine(routine: SkipSpeedupReset());
            return;
        }
        StopCoroutine(_typewriterCoroutine);
        mainTextDisplay.maxVisibleCharacters = mainTextDisplay.textInfo.characterCount;
        CompleteTextRevealed?.Invoke();
    }
    IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => mainTextDisplay.maxVisibleCharacters == mainTextDisplay.textInfo.characterCount - 1);
        currentlySkipping = false;
    }
    public void TempSkip()
    {
        if (quickSkip)
        {
            Skip();
        }
        else
        {
            quickSkip = true;
            currentlySkipping = true;
            Skip();
            quickSkip = false;
            currentlySkipping = false;
        }
    }
    public void DisableSkip()
    {
        currentlySkipping = false;
    }
    public void CheckQuickSkip()
    {
        quickSkip = quickSkipToggle.isOn;
    }

    public void ToggleDyslexicFont()
    {
        useDyslexicFont = dyslexicFontToggle.isOn;
        mainTextDisplay.font = useDyslexicFont ? dyslexicFont : defaultFont;
        optionsText1.font = useDyslexicFont ? dyslexicFont : defaultFont;
        optionsText2.font = useDyslexicFont ? dyslexicFont : defaultFont;
        optionsText3.font = useDyslexicFont ? dyslexicFont : defaultFont;
        optionsText4.font = useDyslexicFont ? dyslexicFont : defaultFont;
    }
    public void ToggleFontColour()
    {
        useBlackFont = fontColourToggle.isOn;
        mainTextDisplay.color = useBlackFont ? fontBlack : fontWhite;
        titleTextDisplay.color = useBlackFont ? fontBlack : fontWhite;
    }
    #region Scroll Stuff
    public void ScrollToVisibleTextPosition()
    {
        if (!scrollRect || !mainTextDisplay || !viewport || !content)
        {
            Debug.LogWarning("Required components not found!");
            return;
        }

        int maxVisibleChars = mainTextDisplay.maxVisibleCharacters;
        string fullText = mainTextDisplay.text;
        int currentCharCount = fullText.Length;

        float visibilityRatio = maxVisibleChars > 0
            ? Mathf.Min(1f, (float)maxVisibleChars / currentCharCount)
            : 1f;

        Vector2 targetNormalizedPos = CalculateScrollPosition(visibilityRatio);

        StartCoroutine(SmoothScroll(targetNormalizedPos));
    }

    private Vector2 CalculateScrollPosition(float visibilityRatio)
    {
        float normalizedPosY = 1f - visibilityRatio;

        return new Vector2(
            0f, normalizedPosY
        );
    }

    private IEnumerator SmoothScroll(Vector2 targetPos)
    {
        const float duration = 0.3f;
        float elapsed = 0f;
        Vector2 startPos = scrollRect.normalizedPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            t = t * t * (3f - 2f * t);

            scrollRect.normalizedPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        scrollRect.normalizedPosition = targetPos;
    }

    private void OnTextChanged()
    {
        ScrollToVisibleTextPosition();
    }
    #endregion
    private void Awake()
    {
        CreateSingleton();

        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);

        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        _textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }
    private void Start()
    {
        viewport = scrollRect.viewport;
        content = scrollRect.content;

        ToggleDyslexicFont();
        ToggleFontColour();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Pre: "+"MaxVisibleCharacters: " + mainTextDisplay.maxVisibleCharacters + " | " + "MainText CharacterCount: " + mainTextDisplay.textInfo.characterCount);
            if (mainTextDisplay.maxVisibleCharacters != mainTextDisplay.textInfo.characterCount - 1)
            {
                //Debug.Log("Post: " + "MaxVisibleCharacters: " + mainTextDisplay.maxVisibleCharacters + " | " + "MainText CharacterCount: " + mainTextDisplay.textInfo.characterCount);
                Skip();
            }
        }
    }
}