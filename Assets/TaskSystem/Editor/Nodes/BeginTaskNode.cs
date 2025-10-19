using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[UseWithGraph(typeof(TaskGraph)), Serializable]
public class BeginTaskNode : Node, ITaskNode
{
    protected TaskOptions taskOptions;
    protected IPort taskOutputPort;

    protected TaskSO task;

    protected override void OnDefineOptions(IOptionDefinitionContext context)
    {
        taskOptions = TaskOptions.AddOptions(context, false);
    }

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        taskOutputPort = TaskGraphUtility.AddTaskOutputPort(context);
    }

    public TaskSO CreateTask()
    {
        task = taskOptions.GetTask();
        return task;
    }

    public TaskSO GetTask() => task;

    public void AssignRelativeTasks()
    {
        task.nextTasks = TaskGraphUtility.GetConnectedTasks(taskOutputPort);
    }
}
