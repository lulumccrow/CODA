using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimatedText : MonoBehaviour
{
    public SpriteRenderer bubbleBackground;
    public BlinkingText continueText;
    public TextMeshProUGUI speechBubbleText;
    public string targetText;
    public float typingTime;

    public void FadeUp()
    {
        StartCoroutine(DoFadeUp());
    }
    public void StartTyping()
    {
        StartCoroutine(DoTyping());
    }

    private IEnumerator DoFadeUp()
    {
        var elapsedTime = 0.0f;
        var time = 0.3f;
        while (elapsedTime < time)
        {
            bubbleBackground.color = new Color(
                bubbleBackground.color.r,
                bubbleBackground.color.g,
                bubbleBackground.color.b,
                Mathf.Lerp(0.0f, 1.0f, (elapsedTime / time))
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator DoTyping() {
        foreach (var character in targetText)
        {
            speechBubbleText.text += character;
            yield return new WaitForSeconds(typingTime);
        }

        continueText.enabled = true;
    }
}
