using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : ScriptableObject
{
    [SerializeField] public string description;
    [SerializeField] public List<TaskSO> initialTasks = new();
    
    public void Init(TaskSO baseTask)
    {
        description = baseTask.description;
        initialTasks = new List<TaskSO>(baseTask.nextTasks);
    }
}