using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interaction : MonoBehaviour
{
    [SerializeField] public Sprite indicator;

    private List<InteractionResponse> responses = new List<InteractionResponse>();
    private PlayerInteract currentPlayer;

    private void Awake()
    {
        responses.AddRange(GetComponents<InteractionResponse>());
    }

    public void TriggerResponses()
    {
        foreach (var response in responses)
        {
            response.TryTriggerResponse();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerInteract = other.GetComponent<PlayerInteract>();
        if (playerInteract != null)
        {
            currentPlayer = playerInteract;
            playerInteract.RegisterObject(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var playerInteract = other.GetComponent<PlayerInteract>();
        if (playerInteract != null)
        {
            currentPlayer = null;
            playerInteract.UnregisterObject(this);
        }
    }
}