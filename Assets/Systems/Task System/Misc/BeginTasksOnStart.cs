using System;
using System.Collections.Generic;
using UnityEngine;

public class BeginTasksOnStart : MonoBehaviour
{
    [SerializeField] TaskManager taskManager;
    [SerializeField] List<TaskSystem> taskSystems;

    private void Start()
    {
        foreach (var taskSystem in taskSystems)
        {
            taskManager.BeginTask(taskSystem);
        }
    }
}
