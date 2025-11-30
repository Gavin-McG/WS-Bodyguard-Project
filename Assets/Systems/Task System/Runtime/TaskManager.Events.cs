using UnityEngine;
using UnityEngine.Events;

public partial class TaskManager
{
    /// <summary>
    /// Data struct for Task related events
    /// </summary>
    [System.Serializable]
    public struct TaskEventData
    {
        public int taskIndex;
        public TaskSO task;

        public TaskEventData(int taskIndex, TaskSO task)
        {
            this.taskIndex = taskIndex;
            this.task = task;
        }
    }

    /// <summary>
    /// Data struct for Task requirement related events
    /// </summary>S
    [System.Serializable]
    public struct RequirementEventData
    {
        public int taskIndex;
        public int requirementIndex;
        public TaskSO task;

        public RequirementEventData(int taskIndex, int requirementIndex, TaskSO task)
        {
            this.taskIndex = taskIndex;
            this.requirementIndex = requirementIndex;
            this.task = task;
        }
    }
    
    [System.Serializable] public class TaskEvent : UnityEvent<TaskEventData> { }
    [System.Serializable] public class RequirementEvent : UnityEvent<RequirementEventData> { }

    [HideInInspector] public TaskEvent OnTaskAdded = new();
    [HideInInspector] public TaskEvent OnTaskCompleted = new();
    [HideInInspector] public TaskEvent OnTaskRemoved = new();
    [HideInInspector] public RequirementEvent OnRequirementCompleted = new();
}