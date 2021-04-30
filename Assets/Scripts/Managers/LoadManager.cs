using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private GameObject loadAnim = null;

    public void LoadNextLevelWithAnim()
    {
        StartCoroutine(LoadLevelWithAnim());
    }

    IEnumerator LoadLevelWithAnim()
    {
        loadAnim.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        operation.allowSceneActivation = false;

        // When the button animation has finished and the next scene has been loaded, jump to the next scene.
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f && loadAnim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                operation.allowSceneActivation = true;
            yield return null;
        }
    }

    /*public void LoadNextLevel()
    {
        LoadLevel();
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            bool condition = transitionCondition();
            if (operation.progress >= 0.9f && condition)
                operation.allowSceneActivation = true;
            yield return null;
        }
    }

    private bool transitionCondition()
    {
        if (rawImage != null)
        {
            VideoPlayer vp = rawImage.GetComponent<VideoPlayer>();
            return vp.frame >= (long)vp.frameCount - 1;
        }
        return true;
    }*/
}
