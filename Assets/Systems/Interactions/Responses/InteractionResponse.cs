using System;
using UnityEngine;
using WolverineSoft.DialogueSystem;

[RequireComponent(typeof(Interaction))]
public abstract class InteractionResponse : MonoBehaviour
{
    [SerializeField] public bool TriggerOnce = false;
    [HideInInspector] public bool hasTriggered = false;

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
