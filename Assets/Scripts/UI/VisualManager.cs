using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : SingletonMonobehaviour<VisualManager>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI messageText;

    public IEnumerator screenFader(float startFadeAlpha, float targetFadeAlpha, float fadeSeconds, Color backgroundColor) {

        Image image = canvasGroup.GetComponentInChildren<Image>();
        image.color = backgroundColor;

        float time = 0;

        while (time <= fadeSeconds)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startFadeAlpha, targetFadeAlpha, time / fadeSeconds);
            yield return null;
        }
    }

    public IEnumerator displayMessage(string text, Color textColor, float displaySeconds)
    {
        // Set text
        messageText.SetText(text);
        messageText.color = textColor;

        if (displaySeconds > 0f)
        {
            float timer = displaySeconds;

            while (timer > 0f && !Input.GetKeyDown(KeyCode.Return))
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (!Input.GetKeyDown(KeyCode.Return))
            {
                yield return null;
            }
        }

        yield return null;

        messageText.SetText("");
    }
}
