using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TaskTester : MonoBehaviour
{
    [SerializeField] private TaskManager manager;
    [SerializeField] private TaskSystem taskSystem;
    [SerializeField] private RequirementSO requirement;
    [Space(10)]
    [SerializeField] private InputActionReference input1;
    [SerializeField] private InputActionReference input2;
    
    private void Update()
    {
        if (input1.action.triggered)
        {
            manager.BeginTask(taskSystem);
        }

        if (input2.action.triggered)
        {
            requirement.CompleteRequirement();
        }
    }
}
