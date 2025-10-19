using UnityEngine.Events;

public partial class TaskManager
{
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

    public TaskEvent OnTaskAdded = new();
    public TaskEvent OnTaskCompleted = new();
    public RequirementEvent OnRequirementCompleted = new();
}