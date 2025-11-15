using UnityEngine;

public class TeleportObjectResponse : InteractionResponse
{
    [SerializeField] public GameObject objectToTeleport;
    [SerializeField] public Transform teleportPosition;

    protected override void TriggerResponse()
    {
        objectToTeleport.transform.position = teleportPosition.position;
    }
}
