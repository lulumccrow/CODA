using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource fxAudioSource;
    public AudioClip introClip;
    public AudioClip loopClip;

    public AudioClip successClip;
    public AudioClip failureClip;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySuccessFX()
    {
        fxAudioSource.volume = 1f;
        fxAudioSource.PlayOneShot(successClip);
    }
    public void PlayFailureFX()
    {
        fxAudioSource.volume = 0.2f;
        fxAudioSource.PlayOneShot(failureClip);
    }

    public void BeginMusic()
    {
        StartCoroutine(MusicCoroutine());
    }
    private IEnumerator MusicCoroutine()
    {
        musicAudioSource.clip = introClip;
        musicAudioSource.Play();

        while(!musicAudioSource.isPlaying)
        {
            yield return null;
        }

        while (true)
        {
            while(musicAudioSource.isPlaying)
            {
                yield return new WaitForSeconds(0.2f);
            }

            musicAudioSource.clip = loopClip;
            musicAudioSource.Play();

            while (!musicAudioSource.isPlaying)
            {
                yield return null;
            }
        }
    }
}

