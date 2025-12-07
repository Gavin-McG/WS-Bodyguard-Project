using System;
using System.Collections.Generic;
using UnityEngine;
using WolverineSoft.DialogueSystem;

public class DSEventChangeActive : MonoBehaviour
{
    [Serializable]
    struct ChangeActiveEntry
    {
        public GameObject target;
        public bool active;
    }
    
    [SerializeField] private DSEvent triggerEvent;
    [SerializeField] private List<ChangeActiveEntry> entries;

    private void OnEnable()
    {
        triggerEvent?.AddListener(TriggerEvent);
    }

    private void OnDisable()
    {
        triggerEvent?.RemoveListener(TriggerEvent);
    }

    private void TriggerEvent()
    {
        foreach (var entry in entries)
        {
            entry.target.SetActive(entry.active);
        }
    }
}
