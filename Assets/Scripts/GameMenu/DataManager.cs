using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManager
{
    public static DataManager instance;
    [HideInInspector] public Save saveInfo;

    public static DataManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("DataManager实例不存在！");
            return instance;
        }
        return instance;
    }

    public DataManager()
    {
        instance = this;
    }

    // Object(Save.Type) --> JSON(string)
    private void SaveByJson(Save saveObj)
    {
        string JsonStr = JsonUtility.ToJson(saveObj);

        StreamWriter sw = new StreamWriter(Application.dataPath + "/JSONData.text");
        sw.Write(JsonStr);
        sw.Close();
    }

    // JSON(String) --> Object(Save.Type)
    private Save LoadByJson()
    {
        Save saveObj = new Save();
        if (File.Exists(Application.dataPath + "/JSONData.text"))
        {
            StreamReader sr = new StreamReader(Application.dataPath + "/JSONData.text");
            string JsonStr = sr.ReadToEnd();
            sr.Close();

            // Convert JSON to Object(Save)
            saveObj = JsonUtility.FromJson<Save>(JsonStr);
        }
        else
        {
            Debug.Log("NOT FOUND FILE");
            return null;
        }
        return saveObj;
    }

    public void SaveGame()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Vector2 playerPos = GameObject.FindGameObjectWithTag(TagContants.PLAYER).transform.position;

        Save saveObj = new Save();
        saveObj.sceneNum = sceneIndex;
        saveObj.playerPos = playerPos;
        SaveByJson(saveObj);

        Debug.Log("file location:" + Application.dataPath + "/JSONData.text");
    }

    public bool LoadGame()
    {
        Save saveObj = LoadByJson();
        if (saveObj != null)
        {
            saveInfo = saveObj;
            return true;
        }
        else
            return false;
    }

    public void DeleteFile(string filePath)
    {
        File.Delete(filePath);
    }
}
