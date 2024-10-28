using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointUI : MonoBehaviour
{
    public Checkpoint checkpoint;
    public Button teleportButton;
    public TextMeshProUGUI checkpointName;

    private void Awake()
    {
        teleportButton.onClick.AddListener(OnTeleportButtonClicked);
    }

    private async void OnTeleportButtonClicked()
    {
        GetComponentInParent<CheckpointManager>().gameObject.SetActive(false);
        await SceneMnager.Instance.Teleport(checkpoint.transform.position);
    }
}
