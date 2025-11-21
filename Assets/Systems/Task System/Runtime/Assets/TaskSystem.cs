using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : ScriptableObject
{
    [SerializeField] public string description;
    [SerializeField] public List<TaskSO> initialTasks = new();
    
    public void Init(List<TaskSO> initialTasks, string description)
    {
        this.description = description;
        this.initialTasks = new List<TaskSO>(initialTasks);
    }
}