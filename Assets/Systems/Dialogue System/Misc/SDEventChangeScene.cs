using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using WolverineSoft.DialogueSystem.Default;

public class SDEventChangeSceneListener : MonoBehaviour
{
    [SerializeField] private DSEventString sceneChangeEvent;

    private void OnEnable()
    {
        sceneChangeEvent?.AddListener(ChangeScene);
    }

    private void OnDisable()
    {
        sceneChangeEvent?.RemoveListener(ChangeScene);
    }

    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
