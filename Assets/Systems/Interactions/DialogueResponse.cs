using UnityEngine;
using WolverineSoft.DialogueSystem;

public class DialogueResponse : InteractionResponse
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] DialogueAsset dialogueAsset;

    protected override void TriggerResponse()
    {
        dialogueManager.BeginDialogue(dialogueAsset);
    }
}
