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
            this.description = activeTask.task.name;
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
}

