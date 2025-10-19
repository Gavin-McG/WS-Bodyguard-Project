using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manager for managing tasks of the Task System
/// </summary>
public partial class TaskManager : MonoBehaviour
{
    private HashSet<TaskSO> completedTasks = new();
    private HashSet<string> completedTaskNames = new();
    
    private List<ActiveTask> currentTasks = new();
    private Dictionary<TaskSO, ActiveTask> taskDict = new();
    
    /// <summary>
    /// Begin a task by inserting at a specific position in the task list
    /// </summary>
    private bool BeginTask(TaskSO task, int index)
    {
        if (IsActiveRecursive(task))
        {
            Debug.LogWarning("Attempting to start task whose tree has not been completed");
            return false;
        }
        
        // Create new active task
        ActiveTask activeTask = new(task);
        currentTasks.Insert(index, activeTask);
        taskDict.Add(task, activeTask);
        
        // Set active task listener
        activeTask.AddCompleteListener(() => CompleteTask(activeTask.task));
        
        return true;
    }
    
    /// <summary>
    /// Begin a task. Cannot start a task that is already ongoing
    /// </summary>
    public bool BeginTask(TaskSO task) => 
        BeginTask(task, currentTasks.Count);
    
    /// <summary>
    /// Mark task as completed for manual override
    /// (Also used for internal purposes)
    /// </summary>
    public bool CompleteTask(TaskSO task)
    {
        if (!taskDict.TryGetValue(task, out var activeTask))
        {
            Debug.LogWarning("Attempting to complete task that has not begun");
            return false;
        }
        
        // Clear completed task from task list
        int index = currentTasks.IndexOf(activeTask);
        taskDict.Remove(task);
        currentTasks.RemoveAt(index);
        
        // Add task to completed list
        completedTasks.Add(task);
        completedTaskNames.Add(task.name);
        
        // Add next tasks to list
        var nextTasks = task.nextTasks;
        nextTasks.Reverse();
        foreach (var nextTask in nextTasks)
            BeginTask(nextTask, index);

        return true;
    }

    /// <summary>
    /// Clear all the tasks waiting to be completed
    /// </summary>
    public void ClearActiveTasks()
    {
        currentTasks.Clear();
        taskDict.Clear();
    }

    /// <summary>
    /// Clears all active tasks and forgets all completed tasks
    /// </summary>
    public void ClearAllTasks()
    {
        completedTaskNames.Clear();
        ClearActiveTasks();
    }
    
    /// <summary>
    /// Check if task has been completed by taskSO
    /// </summary>
    public bool HasCompleted(TaskSO task) => completedTasks.Contains(task);
    
    /// <summary>
    /// Check if task has been completed by name
    /// </summary>
    public bool HasCompleted(string taskName) => completedTaskNames.Contains(taskName);

    /// <summary>
    /// Check if a task and all its subsequent tasks have been completed
    /// </summary>
    public bool HasCompletedRecursive(TaskSO task)
    {
        return HasCompletedRecursive(task, new HashSet<TaskSO>());
    }

    private bool HasCompletedRecursive(TaskSO task, HashSet<TaskSO> visited)
    {
        if (task == null) return true;

        // Cycle detected — stop infinite recursion
        if (!visited.Add(task))
        {
            Debug.LogWarning($"Cycle detected in task graph at {task.name}");
            return true; // or false depending on desired behavior
        }

        bool hasCompleted = HasCompleted(task);
        foreach (var nextTask in task.nextTasks)
            hasCompleted = hasCompleted && HasCompletedRecursive(nextTask, visited);

        return hasCompleted;
    }
    
    /// <summary>
    /// Check if task in ongoing by taskSO
    /// </summary>
    public bool IsActive(TaskSO task) => taskDict.ContainsKey(task);
    
    /// <summary>
    /// Check if task in ongoing by name
    /// </summary>
    public bool IsActive(string taskName) => taskDict.Keys.Any(task => task.name == taskName);

    /// <summary>
    /// Check if a task or any of its subsequent tasks are ongoing
    /// </summary>
    public bool IsActiveRecursive(TaskSO task)
    {
        return IsActiveRecursive(task, new HashSet<TaskSO>());
    }

    private bool IsActiveRecursive(TaskSO task, HashSet<TaskSO> visited)
    {
        if (task == null) return false;

        if (!visited.Add(task))
        {
            Debug.LogWarning($"Cycle detected in task graph at {task.name}");
            return false;
        }

        bool isActive = IsActive(task);
        foreach (var nextTask in task.nextTasks)
            isActive = isActive || IsActiveRecursive(nextTask, visited);

        return isActive;
    }
    
    /// <summary>
    /// Get the UI information of all the current tasks
    /// </summary>
    public List<TaskInfo> GetActiveTaskInfo() =>
        currentTasks.Select((task)=>new TaskInfo(task)).ToList();
}
