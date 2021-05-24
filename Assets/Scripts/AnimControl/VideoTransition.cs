using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoTransition : MonoBehaviour
{
    public float bgmSpeed = 5.0f;
    private VideoPlayer vp;
    private AudioSource audioSource;

    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
        AudioControl();
        StartCoroutine(LoadLevel());
    }

    private void AudioControl()
    {
        audioSource.Play();
        audioSource.volume = 0.1f;
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (audioSource.volume < AudioManager.audioValue)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, AudioManager.audioValue, Time.deltaTime * bgmSpeed);
            }
            if (vp.frame > 650 && audioSource.volume > 0)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0.0f, Time.deltaTime * bgmSpeed);
            }
            if (operation.progress >= 0.9f && vp.frame >= (long)vp.frameCount - 1)
                operation.allowSceneActivation = true;
            yield return null;
        }
    }
}
