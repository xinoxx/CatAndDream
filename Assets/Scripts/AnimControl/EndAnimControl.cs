using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndAnimControl : MonoBehaviour
{
    public GameObject resultPic;
    public GameObject resultSlider;
    public GameObject happiness, aloneness, blackShelter;
    public GameObject restartText;

    public List<GameObject> resultList = new List<GameObject>();
    public float sliderSpeed = 5.0f;

    [SerializeField] private float fadeSpeed = 5.0f;
    private Image blackImage;
    private Text happinessText, alonessText;
    private Slider slider;
    private bool isFading = false, isCalculating = false, flag = true;
    private float alpha = 1.0f;
    private int[] children;
    private int i = 0;
    private float sliderValue = 0.0f;
    private int result1 = 0;
    private float finished = 0.0f;
    private bool resultOk1 = false, resultOk2 = false;
    private bool restartKey = false;

    void Start()
    {
        blackImage = blackShelter.GetComponent<Image>();
        happinessText = happiness.transform.GetChild(0).GetComponent<Text>();
        alonessText = aloneness.transform.GetChild(0).GetComponent<Text>();
        children = GameObject.FindObjectOfType<EndManager>().haveChild;
        slider = resultSlider.GetComponent<Slider>();
    }

    void Update()
    {
        if (isFading)
        {
            FadeOut();
        }
        if (isCalculating)
        {
            CalculateResult();
        }
        if (restartKey)
        {
            if (Input.GetKey(KeyCode.R))
            {
                DataManager.instance.DeleteFile(Application.dataPath + "/JSONData.text");
                PlayerPrefs.DeleteAll();
                GameManager.instance.MainPage();
                restartKey = false;
            }
        }
    }

    private void FadeOut()
    {
        if (alpha >= 0.0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            blackImage.color = new Color(0, 0, 0, alpha);
        }
        else
        {
            isFading = false;
            isCalculating = true;
        }
    }

    private void CalculateResult()
    {
        if (i < resultList.Count)
        {
            if (children[i] == 1)
            {
                if (flag)
                {
                    GameObject resultObj = resultList[i];
                    resultObj.GetComponent<Image>().color = new Color(255, 255, 255, 1);
                    sliderValue += 0.25f;
                    result1 += 25;
                    flag = false;
                    finished = 0.0f;
                }
                if (!flag)
                {
                    finished += Time.deltaTime;
                }
                if (finished > 0.8f && slider.value < sliderValue)
                {
                    slider.value += sliderSpeed * Time.deltaTime;
                }
                else if (finished > 1.0f && slider.value >= sliderValue)
                {
                    flag = true;
                    finished = 0.0f;
                    i++;
                }
            }
            else
            {
                i++;
            }
        }
        else
        {
            float offset = Mathf.Floor(100.0f * Time.deltaTime);
            float text1 = int.Parse(happinessText.text) + offset;
            float text2 = int.Parse(alonessText.text) + offset;
            int result2 = 100 - result1;
            if (!resultOk1 && text1 >= result1)
            {
                happinessText.text = result1.ToString();
                resultOk1 = true;
            }
            else if (!resultOk1)
            {
                happinessText.text = text1.ToString();
            }
            if (!resultOk2 && text2 >= result2)
            {
                alonessText.text = result2.ToString();
                resultOk2 = true;
            }
            else if (!resultOk2)
            {
                alonessText.text = text2.ToString();
            }
            if (resultOk1 && resultOk2)
            {
                isCalculating = false;
                restartText.SetActive(true);
                restartKey = true;
            }
        }
    }

    public void EnableResultSystem()
    {
        blackShelter.gameObject.SetActive(true);
        resultPic.gameObject.SetActive(true);
        resultSlider.gameObject.SetActive(true);
        happiness.gameObject.SetActive(true);
        aloneness.gameObject.SetActive(true);
        for (int j = 0; j < resultList.Count; j++)
        {
            resultList[j].gameObject.SetActive(true);
        }
        isFading = true;
    }

}
