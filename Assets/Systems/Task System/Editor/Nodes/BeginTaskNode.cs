using System;
using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[UseWithGraph(typeof(TaskGraph)), Serializable]
public class BeginTaskNode : Node
{
    protected INodeOption descriptionOption;
    protected IPort taskOutputPort;

    protected TaskSO task;

    protected override void OnDefineOptions(IOptionDefinitionContext context)
    {
        descriptionOption = context.AddOption<string>("Description").Build();
    }

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        taskOutputPort = TaskGraphUtility.AddTaskOutputPort(context);
    }

    public TaskSystem GetSystem()
    {
        List<IPort> connectedPorts = new();
        taskOutputPort.GetConnectedPorts(connectedPorts);
        List<TaskSO> initialTasks = connectedPorts
            .Select(port => port.GetNode())
            .OfType<ITaskNode>()
            .Select(node => node.GetTask())
            .ToList();

        descriptionOption.TryGetValue(out string description);
        
        TaskSystem system = ScriptableObject.CreateInstance<TaskSystem>();
        system.Init(initialTasks, description);
        return system;
    }
}
