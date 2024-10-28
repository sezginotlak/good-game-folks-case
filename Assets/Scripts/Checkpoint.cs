using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    TextMeshPro nameText;

    public string checkpointName;

    private void Start()
    {
        nameText.SetText(checkpointName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            nameText.gameObject.SetActive(true);
            FindObjectOfType<CheckpointManager>(true).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            nameText.gameObject.SetActive(false);
            FindObjectOfType<CheckpointManager>(true).gameObject.SetActive(false);
        }
    }
}
