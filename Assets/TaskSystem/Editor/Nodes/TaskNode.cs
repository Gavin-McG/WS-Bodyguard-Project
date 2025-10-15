using System;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[UseWithGraph(typeof(TaskGraph)), Serializable]
public class TaskNode : ContextNode, ITaskNode
{
    protected TaskOptions taskOptions;
    protected IPort taskInputPort;
    protected IPort taskOutputPort;

    protected TaskSO task;

    protected override void OnDefineOptions(IOptionDefinitionContext context)
    {
        taskOptions = TaskOptions.AddOptions(context);
    }

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        taskInputPort = TaskGraphUtility.AddTaskInputPort(context);
        taskOutputPort = TaskGraphUtility.AddTaskOutputPort(context);
    }

    public TaskSO CreateTask()
    {
        task = taskOptions.GetTask();
        AssignRequirements();
        return task;
    }

    public TaskSO GetTask() => task;

    public void AssignNextTasks()
    {
        task.nextTasks = TaskGraphUtility.GetConnectedTasks(taskOutputPort);
    }

    private void AssignRequirements()
    {
        task.requirements = blockNodes.OfType<RequirementNode>()
            .Select(node => node.GetRequirement())
            .ToList();
    }
}
