using System.Collections.Generic;
using UnityEngine;

public class SetActiveResponse : InteractionResponse
{
    [SerializeField] List<GameObject> objects;
    [SerializeField] bool active;

    protected override void TriggerResponse()
    {
        foreach (var obj in objects)
        {
            obj.SetActive(active);
        }
    }
}
