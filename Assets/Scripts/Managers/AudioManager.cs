using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;

    [HideInInspector] public float audioValue = 0.8f;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            if (instance != null) Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource.volume = audioValue;
    }

    /// <summary>
    /// 修改音量大小
    /// </summary>
    /// <param name="value">当前音量的值</param>
    public void VolumnControl(float value)
    {
        audioSource.volume = value;
        audioValue = value;
    }
}
