using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;
    [SerializeField] public GameObject loadAnim = null;

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

    public void LoadContinueLevel()
    {
        Save saveObj = DataManager.instance.saveInfo;
        StartCoroutine(LoadWithAnim(saveObj.sceneNum, loadAnim));
    }

    public bool CheckData()
    {
        bool result = DataManager.instance.LoadGame();
        return result;
    }

    IEnumerator LoadWithAnim(int sceneNum, GameObject anim)
    {
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
        
        // 清空UI栈内的panel
        GameRoot.GetInstance().UIManager_Root.Pop(true);
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
