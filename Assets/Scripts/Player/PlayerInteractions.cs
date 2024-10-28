using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    InventoryManager inventoryManager;
    PlayerHealth health;
    public LevelSaveSystem LevelSaveSystem { get; set; }

    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        health = GetComponent<PlayerHealth>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AbstractBaseCollectible collectible))
        {
            inventoryManager.AddItem(collectible.item);
            LevelSaveSystem.UpdateLevelObjectStatus(collectible.GetComponent<LevelObject>());
            collectible.gameObject.SetActive(false);
        }

        if (collision.TryGetComponent(out AbstractBaseEnemy enemy))
        {
            health.GetDamage(enemy.damage);
            Destroy(enemy.gameObject);
        }
    }
}
