using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float maxHealth = 100f;
    public float CurrentHealth { get; set; }

    public void GetDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        UpdateHealthUI(CurrentHealth);

        if (CurrentHealth <= 0)
            PlayerCanvasManager.Instance.OpenGameOverPanel();
    }

    public void UpdateHealthUI(float currentHealth)
    {
        PlayerCanvasManager.Instance.UpdateHealthBar(currentHealth / maxHealth);
    }
}
