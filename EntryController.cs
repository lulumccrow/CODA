using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EntryController : MonoBehaviour
{
    public MovementScript movementScript;
    public GameObject firstBubble;
    public GameObject secondBubble;
    private bool activated = false;
    public SpriteRenderer fadeSprite;
    private Animator personAnimator;


    public void Start()
    {
        movementScript = FindObjectOfType<MovementScript>();
        personAnimator = movementScript.gameObject.GetComponent<Animator>();
        personAnimator.Play("Base Layer.C-Run");
        StartCoroutine(DoWalk());
        DontDestroyOnLoad(movementScript.gameObject);
        DontDestroyOnLoad(fadeSprite.gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator DoWalk()
    {
        yield return movementScript.MoveTo(new Vector2(-5.17f, -0.84f), 3.0f);
        personAnimator.Play("Base Layer.C-Idle-Animation");

        if(!firstBubble.activeSelf)
        {
            firstBubble.SetActive(true);
            secondBubble.SetActive(false);
            activated = false;
        }
        firstBubble.GetComponent<AnimatedText>().FadeUp();
        firstBubble.GetComponent<AnimatedText>().StartTyping();
    }

    public void OnMouseDown()
    {
        if (!activated)
        {
            firstBubble.SetActive(false);
            secondBubble.SetActive(true);
            secondBubble.GetComponent<AnimatedText>().StartTyping();
            activated = true;
        }
        else
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        var elapsedTime = 0.0f;
        var time = 2.0f;
        while (elapsedTime < time)
        {
            fadeSprite.color = new Color(
                fadeSprite.color.r,
                fadeSprite.color.g,
                fadeSprite.color.b,
                Mathf.Lerp(0.0f, 1.0f, (elapsedTime / time))
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("MainScene");

        elapsedTime = 0.0f;
        time = 2.0f;
        while (elapsedTime < time)
        {
            fadeSprite.color = new Color(
                fadeSprite.color.r,
                fadeSprite.color.g,
                fadeSprite.color.b,
                Mathf.Lerp(1.0f, 0.0f, (elapsedTime / time))
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
