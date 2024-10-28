using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveSystem : AbstractBaseSave
{
    [SerializeField]
    Transform playerTransform;

    PlayerHealth playerHealth;
    InventoryManager inventoryManager;

    public static PlayerSaveSystem Instance;

    private void Awake()
    {
        saveID = "PlayerSaveData";

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public override void Start()
    {
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        inventoryManager = playerTransform.GetComponent<InventoryManager>();

        base.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Save();
        }
    }

    public override void Save()
    {
        SaveData saveData = new SaveData();
        saveData.playerPosition = playerTransform.position;
        saveData.playerHealth = playerHealth.CurrentHealth;
        saveData.inventoryData = inventoryManager.inventoryItemDataList;
        saveData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        foreach (InventorySlot slot in inventoryManager.inventorySlotList)
        {
            saveData.slotList.Add(slot.InventoryItemData);
        }

        string saveString = JsonUtility.ToJson(saveData); 
        PlayerPrefs.SetString(saveID, saveString);
    }

    public override async void Load()
    {
        string jsonString = PlayerPrefs.GetString(saveID, "");

        if (jsonString == "")
        {
            playerHealth.CurrentHealth = 100f;
            await SceneMnager.Instance.LoadLevel(0, playerTransform.position);
            return;
        }
        SaveData loadData = JsonUtility.FromJson<SaveData>(jsonString);

        await SceneMnager.Instance.LoadLevel(loadData.sceneIndex, loadData.playerPosition);
        
        playerHealth.CurrentHealth = loadData.playerHealth;
        inventoryManager.inventoryItemDataList = loadData.inventoryData;
        
        for (int i = 0; i < loadData.slotList.Count; i++)
        {
            inventoryManager.inventorySlotList[i].InventoryItemData = loadData.slotList[i];
        }

        playerHealth.UpdateHealthUI(playerHealth.CurrentHealth);
        if (inventoryManager.inventoryItemDataList.Count > 0)
            inventoryManager.UpdateSlotsAfterLoad();
    }
}

[Serializable]
public class SaveData
{
    public int sceneIndex;
    public Vector3 playerPosition;
    public float playerHealth;
    public List<InventoryItemData> inventoryData = new List<InventoryItemData>();
    public List<InventoryItemData> slotList = new List<InventoryItemData>();
}
