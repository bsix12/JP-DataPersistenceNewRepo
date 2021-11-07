using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataStorage : MonoBehaviour
{
    public static DataStorage Instance;

    public int highScoreData;
    public string playerInputData;

    private void Awake()                    // awake is called as soon as the object is created
    {
        // this pattern is a singleton
        if (Instance != null)               // the first DataStorage is created when there are none
        {
            Destroy(gameObject);            // any additional DataStorage instances are destroyed if one already exists
            return;
        }
        Instance = this;                    // 'this' is stored in the class member Instance, the current instance of DataStorage
                                            // DataStorage.Instance can now be called from any other script.  Don't need to reference it.
        DontDestroyOnLoad(gameObject);      // marked to persist across scene changes
        LoadDataFromDisk();
    }

    [System.Serializable]
    class SaveData
    {
        public int highScoreData;
        public string playerInputData;
    }

    public void SaveDataToDisk()
    {
        SaveData data = new SaveData();     // new instance of the save data
        data.highScoreData = highScoreData;
        data.playerInputData = playerInputData;

        string json = JsonUtility.ToJson(data);         // serialization to json

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);         //write sting to file.  where to write, what to write
    }

    public void LoadDataFromDisk()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);  // deserialization from json

            highScoreData = data.highScoreData;
            playerInputData = data.playerInputData;
        }
    }
}
