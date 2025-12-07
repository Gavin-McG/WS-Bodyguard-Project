using System;
using System.Collections;
using UnityEngine;
using WolverineSoft.DialogueSystem;

public class BeginDialogueOnStart : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] DialogueAsset dialogueAsset;
    [SerializeField] float delay = 0f;

    private void Start()
    {
        StartCoroutine(StartDialogueRoutine());
    }

    IEnumerator StartDialogueRoutine()
    {
        yield return new WaitForSeconds(delay);
        dialogueManager.BeginDialogue(dialogueAsset);
    }
}
