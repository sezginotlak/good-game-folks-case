using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    GameObject portalText;

    [SerializeField]
    int toBeLoadedSceneIndex;

    bool isInteractedPlayer;

    public Transform teleportedPosition;

    private async void Update()
    {
        if (!isInteractedPlayer) return;

        if (Input.GetKeyDown(KeyCode.L))
            await SceneMnager.Instance.LoadLevel(toBeLoadedSceneIndex, Vector3.zero, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            isInteractedPlayer = true;
            portalText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            isInteractedPlayer = false;
            portalText.SetActive(false);
        }
    }
}
