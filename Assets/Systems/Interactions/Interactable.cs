using System;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] List<InteractionResponse> responses = new List<InteractionResponse>();
    
    PlayerInteract currentPlayer;
    
    public void TriggerResponses()
    {
        foreach (InteractionResponse response in responses)
        {
            response.TryTriggerResponse();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerInteract playerInteract = other.GetComponent<PlayerInteract>();
        if (playerInteract != null)
        {
            currentPlayer = playerInteract;
            playerInteract.RegisterObject(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInteract playerInteract = other.GetComponent<PlayerInteract>();
        if (playerInteract != null)
        {
            currentPlayer = null;
            playerInteract.UnregisterObject(this);
        }
    }
}
