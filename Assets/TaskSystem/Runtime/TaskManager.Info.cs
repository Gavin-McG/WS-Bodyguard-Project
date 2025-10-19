using System.Collections.Generic;
using System.Linq;

public partial class TaskManager
{
    public class TaskInfo
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

    public class RequirementInfo
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

