using System;
using UnityEngine;
using WolverineSoft.DialogueSystem;

[RequireComponent(typeof(Interaction))]
public abstract class InteractionResponse : MonoBehaviour
{
    [SerializeField] protected bool TriggerOnce = true;

    private bool hasTriggered = false;

    public void TryTriggerResponse()
    {
        if (!TriggerOnce || !hasTriggered)
        {
            hasTriggered = true;
            TriggerResponse();
        }
    }

    protected abstract void TriggerResponse();
}
