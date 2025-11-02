using System.Collections.Generic;
using System.Linq;

public partial class TaskManager
{
    /// <summary>
    /// UI info for a given task
    /// </summary>
    public struct TaskInfo
    {
        public TaskSO task;
        public string description;
        public List<RequirementInfo> requirements;

        public TaskInfo(ActiveTask activeTask)
        {
            this.task = activeTask.task;
            this.description = activeTask.task.description;
            this.requirements = activeTask.task.requirements
                .Select(((req, i) => new RequirementInfo(req, activeTask.completed[i])))
                .ToList();
        }
    }

    /// <summary>
    /// UI info for a given requirement
    /// </summary>
    public struct RequirementInfo
    {
        public string description;
        public bool completed;

        public RequirementInfo(TaskSO.Requirement requirement, bool completed)
        {
            this.description = requirement.description;
            this.completed = completed;
        }
    }
    
    /// <summary>
    /// Get the info of a specific task if it is active
    /// </summary>
    public bool TryGetTaskInfo(TaskSO task, out TaskInfo info)
    {
        if (!taskDict.TryGetValue(task, out var activeTask))
        {
            info = default;
            return false;
        };
        
        info = new TaskInfo(activeTask);
        return true;
    }
    
    /// <summary>
    /// Get the UI information of all the current tasks
    /// </summary>
    public List<TaskInfo> GetAllActiveTaskInfo() =>
        activeTasks.Select((task)=>new TaskInfo(task)).ToList();
}

