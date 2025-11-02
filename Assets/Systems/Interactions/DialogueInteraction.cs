using UnityEngine;
using WolverineSoft.DialogueSystem;

public class DialogueInteraction : InteractionResponse
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] DialogueAsset dialogueAsset;

    protected override void TriggerResponse()
    {
        dialogueManager.BeginDialogue(dialogueAsset);
    }
}
