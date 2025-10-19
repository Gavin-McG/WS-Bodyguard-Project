using System.Collections.Generic;
using UnityEngine;

public class TaskSO : ScriptableObject
{
    [SerializeField] public string description;
    [SerializeField] public List<Requirement> requirements = new();
    [SerializeField] public List<TaskSO> previousTasks = new();
    [SerializeField] public List<TaskSO> nextTasks = new();
    [SerializeField] public bool requirePrevious = false;
    
    [System.Serializable]
    public class Requirement
    {
        [SerializeField] public string description;
        [SerializeField] public RequirementSO requirementSO;
    }
}
