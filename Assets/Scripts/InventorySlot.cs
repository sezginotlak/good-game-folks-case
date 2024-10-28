using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI itemNameText;

    [SerializeField]
    TextMeshProUGUI itemCountText;

    [SerializeField]
    Image itemImage;

    [SerializeField]
    Button removeButton;

    
    public InventoryItemData InventoryItemData { get; set; }

    private void Awake()
    {
        removeButton.onClick.AddListener(RemoveItem);
    }

    private void RemoveItem()
    {
        FindObjectOfType<InventoryManager>().RemoveItem(InventoryItemData.item);
    }

    public void InitSlot(InventoryItemData inventoryItemData)
    {
        InventoryItemData = inventoryItemData;
        itemNameText.SetText(inventoryItemData.item.id);
        itemCountText.SetText("x" +inventoryItemData.count.ToString());
        itemImage.sprite = inventoryItemData.item.sprite;
        removeButton.gameObject.SetActive(true);
    }

    public void AdjustSlotAfterLoad()
    {
        if (InventoryItemData.item == null) return;

        itemNameText.SetText(InventoryItemData.item.id);
        itemCountText.SetText("x" + InventoryItemData.count.ToString());
        itemImage.sprite = InventoryItemData.item.sprite;
        removeButton.gameObject.SetActive(true);
    }

    public void UpdateSlot(InventoryItemData inventoryItemData)
    {
        if (InventoryItemData.item == null) return;

        if (InventoryItemData.count < 1)
        {
            itemNameText.SetText("");
            itemCountText.SetText("");
            itemImage.sprite = null;    
            removeButton.gameObject.SetActive(false);
            InventoryItemData = null;
        }
        else
        {
            itemCountText.SetText("x" + inventoryItemData.count.ToString());
        }
    }
}
