using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public partial class TaskManager
{
    /// <summary>
    /// Helper class for managing tasks and their requirements
    /// </summary>
    public class ActiveTask
    {
        public UnityEvent OnTaskCompleted = new();
        public UnityEvent<int> OnRequirementCompleted = new();

        public readonly TaskSO task;
        public List<bool> completed;
        private int completedCount;
        private List<UnityAction> callbacks;

        public ActiveTask(TaskSO task)
        {
            this.task = task;
            this.completed = task.requirements.Select(_ => false).ToList();
            this.completedCount = 0;
            this.callbacks = new List<UnityAction>();

            // Assign listeners for each requirement
            for (int i = 0; i < task.requirements.Count; i++)
            {
                int index = i;
                UnityAction callback = () => CompleteRequirement(index);
                task.requirements[index].requirementSO.OnRequirementCompleted.AddListener(callback);
                callbacks.Add(callback);
            }
        }

        private bool CompleteRequirement(int index)
        {
            // Prevent multiple completions for the same requirement
            if (completed[index]) return false;

            completed[index] = true;
            completedCount++;

            // Notify listeners that a requirement was completed
            OnRequirementCompleted.Invoke(index);

            // If all requirements are done, trigger full completion
            if (completedCount == task.requirements.Count)
            {
                OnTaskCompleted.Invoke();
                ClearListeners();
            }

            return true;
        }

        public void ClearListeners()
        {
            OnRequirementCompleted.RemoveAllListeners();
            OnTaskCompleted.RemoveAllListeners();

            for (int i = 0; i < task.requirements.Count; i++)
            {
                task.requirements[i].requirementSO.OnRequirementCompleted.RemoveListener(callbacks[i]);
            }
            callbacks.Clear();
        }
    }
}