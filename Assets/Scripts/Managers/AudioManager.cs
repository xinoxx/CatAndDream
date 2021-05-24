using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Slider audioSlider;
    public AudioSource audioSource;

    [HideInInspector] public static float audioValue = 1.0f;

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

    public void VolumnControl()
    {
        audioSource.volume =  audioSlider.value;
        audioValue = audioSlider.value;
    }
}
