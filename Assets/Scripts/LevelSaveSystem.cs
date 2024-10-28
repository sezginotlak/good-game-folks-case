using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSaveSystem : AbstractBaseSave
{
    public static LevelSaveSystem Instance;
    public List<LevelObject> levelObjectList = new List<LevelObject>();

    private void Awake()
    {
        saveID = "LevelSaveData" + SceneManager.GetActiveScene().buildIndex;

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public override void Start()
    {
        FindObjectOfType<PlayerInteractions>().LevelSaveSystem = this;
        base.Start();
    }

    public void UpdateLevelObjectStatus(LevelObject item)
    {
        item.isDestroyed = true;
    }

    public override void Save()
    {
        LevelData saveData = new LevelData();
        List<LevelObjectData> levelObjectDataList = new List<LevelObjectData>();
        foreach(LevelObject levelObject in levelObjectList)
        {
            LevelObjectData levelObjectData = new LevelObjectData();
            levelObjectData.id = levelObject.id;
            levelObjectData.isDestroyed = levelObject.isDestroyed;

            levelObjectDataList.Add(levelObjectData);
        }
        saveData.levelObjectDataList = levelObjectDataList;

        string saveString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(saveID, saveString);
    }

    public override void Load()
    {
        string jsonString = PlayerPrefs.GetString(saveID, "");

        if (jsonString == "")
        {
            //GetLevelObjects();
            return;
        }
        LevelData loadData = JsonUtility.FromJson<LevelData>(jsonString);

        foreach (LevelObjectData levelObject in loadData.levelObjectDataList)
        {
            LevelObject levelObj = levelObjectList.Where(x => x.id == levelObject.id).FirstOrDefault();

            if (levelObj == null) continue;
            
            levelObj.isDestroyed = levelObject.isDestroyed;
        }

        foreach (LevelObject levelObject in levelObjectList)
        {
            if (!levelObject.isDestroyed) continue;

            levelObject.gameObject.SetActive(false);
        }
    }
}

[Serializable]
public class LevelData
{
    public List<LevelObjectData> levelObjectDataList = new List<LevelObjectData>();
}

[Serializable]
public class LevelObjectData
{
    public string id;
    public bool isDestroyed;
}

