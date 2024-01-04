using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class MainController : MonoBehaviour
{
    public SpriteRenderer fadeSprite;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(fadeSprite);
    }

    public void TransitionToQuitScene()
    {
        StartCoroutine(TransitionScene());
    }

    private IEnumerator TransitionScene()
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

        Destroy(FindObjectOfType<MovementScript>().gameObject);
        SceneManager.LoadScene("QuitScene");

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

