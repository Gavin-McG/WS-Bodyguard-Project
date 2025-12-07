using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using WolverineSoft.DialogueSystem;
using WolverineSoft.DialogueSystem.Values;

public class LightFailCondition : MonoBehaviour
{
    [SerializeField] private DSValue hasFixedLight;
    [SerializeField] private StartRhythmGameResponse response;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private string loadScene;

    private void OnEnable()
    {
        response.end_song.AddListener(CheckCondition);
    }

    private void OnDisable()
    {
        response.end_song.RemoveListener(CheckCondition);
    }

    private void CheckCondition(int misses)
    {
        if (misses < 3)
        {
            hasFixedLight.SetValue(dialogueManager, DSValue.ValueScope.Global, 1);
        }
        SceneManager.LoadScene(loadScene);
    }
}
