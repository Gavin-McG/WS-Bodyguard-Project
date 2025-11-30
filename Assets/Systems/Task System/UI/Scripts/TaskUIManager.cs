using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskUIManager : MonoBehaviour
{
    [SerializeField] TaskManager taskManager;
    [SerializeField] Transform taskList;
    [SerializeField] TaskUI taskPrefab;
    
    private List<TaskUI> tasks = new();

    private void OnEnable()
    {
        taskManager.OnTaskAdded.AddListener(TaskAdded);
        taskManager.OnTaskCompleted.AddListener(TaskCompleted);
        taskManager.OnTaskRemoved.AddListener(TaskRemoved);
        taskManager.OnRequirementCompleted.AddListener(RequirementCompleted);
    }

    private void OnDisable()
    {
        taskManager.OnTaskAdded.RemoveListener(TaskAdded);
        taskManager.OnTaskCompleted.RemoveListener(TaskCompleted);
        taskManager.OnTaskRemoved.RemoveListener(TaskRemoved);
        taskManager.OnRequirementCompleted.RemoveListener(RequirementCompleted);
    }

    private void TaskAdded(TaskManager.TaskEventData data)
    {
        TaskUI newTask = Instantiate(taskPrefab, taskList);
        taskManager.TryGetTaskInfo(data.task, out var taskInfo);
        newTask.Init(taskInfo);
        tasks.Insert(data.taskIndex, newTask);
    }

    private void TaskCompleted(TaskManager.TaskEventData data)
    {
        TaskUI taskUI = tasks[data.taskIndex];
        tasks.RemoveAt(data.taskIndex);
        taskUI.SucceedTask();
    }

    private void TaskRemoved(TaskManager.TaskEventData data)
    {
        TaskUI taskUI = tasks[data.taskIndex];
        tasks.RemoveAt(data.taskIndex);
        taskUI.FailedTask();
    }

    private void RequirementCompleted(TaskManager.RequirementEventData data)
    {
        TaskUI taskUI = tasks[data.taskIndex];
        taskUI.SucceedRequirement(data);
    }
}
