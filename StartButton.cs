using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public SpriteRenderer fadeSprite;
    public void PlayButton()
    {
        StartCoroutine(SceneFade());
    }

    private void Start()
    {
        DontDestroyOnLoad(fadeSprite.gameObject);
    }

    private IEnumerator SceneFade()
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

        SceneManager.LoadScene("EntryScene");

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
