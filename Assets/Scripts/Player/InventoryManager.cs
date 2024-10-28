using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItemData> inventoryItemDataList = new List<InventoryItemData>();
    public List<InventorySlot> inventorySlotList = new List<InventorySlot>();

    public void AddItem(Item item)
    {
        InventoryItemData itemData = inventoryItemDataList.Where(x => x.item.id == item.id).FirstOrDefault();
        InventorySlot inventorySlot;
        if (itemData != null)
            inventorySlot = inventorySlotList.Where(x => x.InventoryItemData.item.id == itemData.item.id).FirstOrDefault();
        else
            inventorySlot = inventorySlotList[inventoryItemDataList.Count];

        if (itemData == null)
        {
            InventoryItemData newData = new InventoryItemData();
            newData.item = item;
            newData.count = 1;
            inventoryItemDataList.Add(newData);
            inventorySlot.InitSlot(newData);
        }
        else
        {
            itemData.count++;
            inventorySlot.UpdateSlot(itemData);
        }
    }

    public void RemoveItem(Item item)
    {
        InventoryItemData itemData = inventoryItemDataList.Where(x => x.item.id == item.id).FirstOrDefault();
        InventorySlot inventorySlot = inventorySlotList.Where(x => x.InventoryItemData == itemData).FirstOrDefault();

        if (itemData == null) return;

        if (itemData.count > 0)
        {
            itemData.count--;
            inventorySlot.UpdateSlot(itemData);

            if (itemData.count == 0)
                inventoryItemDataList.Remove(itemData);
        }
    }

    public void UpdateSlotsAfterLoad()
    {
        foreach (InventorySlot inventorySlot in inventorySlotList)
        {
            inventorySlot.AdjustSlotAfterLoad();
        }
    }
}

[System.Serializable]
public class InventoryItemData
{
    public Item item;
    public int count;
}
