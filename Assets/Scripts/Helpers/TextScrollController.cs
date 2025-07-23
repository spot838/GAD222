using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextScrollController : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private TMP_Text textDisplay;

    [SerializeField] private RectTransform contentRect;
    [SerializeField] private RectTransform viewportRect;
    private float lastContentHeight;
    private float lastViewportHeight;
    private float lastScrollPosition;

    private void Start()
    {
        //contentRect = scrollRect.content.GetComponent<RectTransform>();
        //viewportRect = scrollRect.viewport.GetComponent<RectTransform>();

        // Initialize with centered position
        lastScrollPosition = 0.5f;
        scrollRect.verticalNormalizedPosition = lastScrollPosition;

        // Store initial measurements
        lastContentHeight = contentRect.rect.height;
        lastViewportHeight = viewportRect.rect.height;
    }

/*    private void LateUpdate()
    {
        // Check if content or viewport has changed size
        if (contentRect.rect.height != lastContentHeight ||
            viewportRect.rect.height != lastViewportHeight)
        {
            UpdateScrollPosition();
            lastContentHeight = contentRect.rect.height;
            lastViewportHeight = viewportRect.rect.height;
        }
    }*/

    public void UpdateScrollPosition()
    {
        float totalScrollableArea = contentRect.rect.height - viewportRect.rect.height;

        // During progressive reveal: center visible content
        if (textDisplay.maxVisibleCharacters < textDisplay.textInfo.characterCount)
        {
            float visibleHeight = CalculateVisibleContentHeight();
            float targetOffset = visibleHeight / 2f;
            lastScrollPosition = Mathf.Clamp01(1f - (targetOffset / totalScrollableArea));
        }
        // After full reveal: maintain bottom position
        else
        {
            lastScrollPosition = 1f;
        }

        // Apply position with smooth transition
        scrollRect.verticalNormalizedPosition = lastScrollPosition;
    }

    private float CalculateVisibleContentHeight()
    {
        TMP_TextInfo textInfo = textDisplay.textInfo;
        if (textInfo == null) return 0f;

        int visibleLines = Mathf.Min(
            textInfo.lineCount,
            Mathf.CeilToInt((float)textDisplay.maxVisibleCharacters /
                           textInfo.lineInfo[0].characterCount));

        return visibleLines * textInfo.lineInfo[0].baseline;
    }
}