using UnityEngine;
using System.Collections;

public class BlinkingText : MonoBehaviour
{
    public GameObject targetText;

    private void Start()
    {
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        while(true)
        {
            targetText.SetActive(false);
            yield return new WaitForSeconds(0.8f);
            targetText.SetActive(true);
            yield return new WaitForSeconds(0.8f);
        }
    }
}

