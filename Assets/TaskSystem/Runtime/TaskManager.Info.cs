using System.Collections.Generic;
using System.Linq;

public partial class TaskManager
{
    /// <summary>
    /// UI info for a given task
    /// </summary>
    public struct TaskInfo
    {
        public string description;
        public List<RequirementInfo> requirements;

        public TaskInfo(ActiveTask task)
        {
            this.description = task.task.name;
            this.requirements = task.task.requirements
                .Select(((req, i) => new RequirementInfo(req, task.completed[i])))
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

