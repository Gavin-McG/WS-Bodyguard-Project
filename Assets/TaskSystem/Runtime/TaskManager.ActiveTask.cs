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
        private UnityEvent completeTask;
        
        public readonly TaskSO task;
        public List<bool> completed;
        private int completedCount;
        
        private List<UnityAction> callbacks;
        
        public ActiveTask(TaskSO task)
        {
            this.completeTask = new UnityEvent();
            this.task = task;
            this.completed = task.requirements.Select(_=>false).ToList();
            this.completedCount = 0;
            this.callbacks = new List<UnityAction>();
            
            //assign listeners
            for (int i = 0; i < task.requirements.Count; i++)
            {
                int index = i;
                UnityAction callback = () => CompleteRequirement(index);
                task.requirements[index].requirementSO.AddCompleteListener(callback);
                callbacks.Add(callback);
            }
        }

        ~ActiveTask()
        {
            ClearCallbacks();
        }

        private bool CompleteRequirement(int index)
        {
            if (completed[index]) return false;
            
            ++completedCount;
            completed[index] = true;
            if (completedCount == task.requirements.Count)
            {
                ClearCallbacks();
            }

            return true;
        }

        private void ClearCallbacks()
        {
            for (int i = 0; i < task.requirements.Count; i++)
            {
                task.requirements[i].requirementSO.RemoveCompleteListener(callbacks[i]);
            }
        }
        
        public void AddCompleteListener(UnityAction call) => 
            completeTask.AddListener(call);
        
        public void RemoveCompleteListener(UnityAction call) =>
            completeTask.RemoveListener(call);
        
        public void RemoveAllCompleteListeners() =>
            completeTask.RemoveAllListeners();
    }
}
