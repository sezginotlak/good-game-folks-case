using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField]
    List<Checkpoint> checkpointList;

    [SerializeField]
    Transform checkpointContent;

    [SerializeField]
    CheckpointUI checkpointUIPrefab;

    [SerializeField]
    Button saveButton;

    [SerializeField]
    Button closeButton;

    private void Awake()
    {
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void Start()
    {
        FillContent();
    }

    private void FillContent()
    {
        foreach (Checkpoint checkpoint in checkpointList) 
        {
            CheckpointUI checkpointUI = Instantiate(checkpointUIPrefab, checkpointContent);
            checkpointUI.checkpoint = checkpoint;
            checkpointUI.checkpointName.SetText(checkpoint.checkpointName);
        }
    }

    private void OnSaveButtonClicked()
    {
        PlayerSaveSystem.Instance.Save();
        LevelSaveSystem.Instance.Save();
    }
}
