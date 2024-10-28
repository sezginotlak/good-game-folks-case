using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMnager : MonoBehaviour
{
    public static SceneMnager Instance;

    [SerializeField]
    CanvasGroup fadePanelCanvasGroup;

    [SerializeField]
    Transform playerTransform;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public async Task LoadLevel(int sceneIndex, Vector3 playerPosition, bool isSceneChanged = false)
    {
        await DOTween.To(() => fadePanelCanvasGroup.alpha, x => fadePanelCanvasGroup.alpha = x, 1f, 1f).AsyncWaitForCompletion();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            await Task.Yield();
        }

        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (isSceneChanged)
            playerTransform.position = FindObjectOfType<Portal>().teleportedPosition.position;
        else
            playerTransform.position = playerPosition;


        await DOTween.To(() => fadePanelCanvasGroup.alpha, x => fadePanelCanvasGroup.alpha = x, 0f, 1f).AsyncWaitForCompletion();
    }



    public async Task Teleport(Vector3 position)
    {
        await DOTween.To(() => fadePanelCanvasGroup.alpha, x => fadePanelCanvasGroup.alpha = x, 1f, 1f).AsyncWaitForCompletion();

        playerTransform.position = position;

        await DOTween.To(() => fadePanelCanvasGroup.alpha, x => fadePanelCanvasGroup.alpha = x, 0f, 1f).AsyncWaitForCompletion();
    }
}
