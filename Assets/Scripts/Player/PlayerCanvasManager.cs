using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasManager : MonoBehaviour
{
    public static PlayerCanvasManager Instance;
    [SerializeField]
    GameObject inventoryGameObject;

    [SerializeField]
    Image healthImageFill;

    [SerializeField]
    GameObject restartPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            HandleInventoryVisibility(!inventoryGameObject.activeInHierarchy);
    }

    private void HandleInventoryVisibility(bool isVisible)
    {
        inventoryGameObject.SetActive(isVisible);
    }

    public void UpdateHealthBar(float fillAmount)
    {
        healthImageFill.DOFillAmount(fillAmount, 0.2f);
    }

    public void OpenGameOverPanel()
    {
        restartPanel.SetActive(true);
    }
}
