using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] InputActionReference interactAction;
    [SerializeField] SpriteRenderer indicatorSprite;
    
    private HashSet<Interaction> nearbyObjects = new HashSet<Interaction>();

    private void OnEnable()
    {
        interactAction.action.performed += TriggerNearbyObjects;
    }

    private void OnDisable()
    {
        interactAction.action.performed -= TriggerNearbyObjects;
    }

	private void Update()
    {
        indicatorSprite.sprite = GetClosest()?.indicator;
    }
    
    public void RegisterObject(Interaction obj)
    {
        nearbyObjects.Add(obj);
    }

    public void UnregisterObject(Interaction obj)
    {
        nearbyObjects.Remove(obj);
    }

    private void TriggerNearbyObjects(InputAction.CallbackContext context)
    {
        GetClosest()?.TriggerResponses();
    }

    private Interaction GetClosest()
    {
        Interaction closest = null;
        float closestDistance = float.MaxValue;
        foreach (Interaction obj in nearbyObjects)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (closest == null || distance < closestDistance)
            {
                closest = obj;
                closestDistance = distance;
            }
        }
        
        return closest;
    }
}
