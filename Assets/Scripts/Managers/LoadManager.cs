using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;
    [SerializeField] private GameObject loadAnim = null;

    private bool noDataHint = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            if (instance != null) Destroy(gameObject);
        }
    }

    public void LoadLevelWithAnim(int sceneNum)
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(LoadWithAnim(sceneNum, loadAnim));
    }

    public void LoadTargetLevel(int sceneNum)
    {
        StartCoroutine(LoadLevel(sceneNum));
    }

    public void LoadContinueLevel(GameObject anim)
    {
        bool result = DataManager.instance.LoadGame();
        if (result)
        {
            Save saveObj = DataManager.instance.saveInfo;
            StartCoroutine(LoadWithAnim(saveObj.sceneNum, anim));
        }
        else
            noDataHint = true;
    }

    public void PopHint(GameObject pop)
    {
        if (noDataHint)
            pop.SetActive(true);
    }

    IEnumerator LoadWithAnim(int sceneNum, GameObject anim)
    {
        anim.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNum);
        operation.allowSceneActivation = false;

        // When the button animation has finished and the next scene has been loaded, jump to the next scene.
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f && anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    IEnumerator LoadLevel(int sceneNum)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNum);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
