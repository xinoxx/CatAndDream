using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnEvent : MonoBehaviour
{
    [SerializeField] private int loadSceneIndex = 0;
    private Animator btnAnim;

    void Start()
    {
        btnAnim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckAnimState(loadSceneIndex);
    }

    // When the button animation ends, the next game scene is loaded.
    private void CheckAnimState(int sceneIndex)
    {
        AnimatorStateInfo stateInfo = btnAnim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime > 1.0f)
        {
            SceneManager.LoadScene(sceneIndex);
        }
}
}
