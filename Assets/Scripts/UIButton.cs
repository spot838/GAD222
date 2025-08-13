using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class UIButton : MonoBehaviour
{
    Button button;
    [SerializeField] Sprite buttonUp;
    [SerializeField] Sprite buttonDown;

    public UnityEvent afterButtonDelay;
    public void CallEvent()
    {
        afterButtonDelay.Invoke();
    }

    public void StartOnClicked()
    {
        button.interactable = false;

        button.image.sprite = buttonDown;
        StartCoroutine(ButtonPressDelay());
    }
    IEnumerator ButtonPressDelay()
    {
        yield return new WaitForSeconds(0.3f);
        ContinueOnClicked();
    }

    public void ContinueOnClicked()
    {
        button.image.sprite = buttonUp;

        CallEvent();

        button.interactable = true;
    }


    private void Awake()
    {
        button = GetComponent<Button>();
    }
}