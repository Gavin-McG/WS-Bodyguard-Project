using System.Collections.Generic;
using UnityEngine;

public class TaskSO : ScriptableObject
{
    [SerializeField] public string description;
    [SerializeField] public List<Requirement> requirements;
    [SerializeField] public List<TaskSO> nextTasks;
    
    [System.Serializable]
    public class Requirement
    {
        [SerializeField] public string description;
        [SerializeField] public RequirementSO requirementSO;
    }
}
