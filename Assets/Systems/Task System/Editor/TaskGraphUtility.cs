using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEngine;

public static class TaskGraphUtility
{
    private const string TaskInputPortName = "Input Tasks";
    private const string TaskOutputPortName = "Output Tasks";
    
    private const string TaskInputPortDisplayName = "Tasks";
    private const string TaskOutputPortDisplayName = "Tasks";
    
    // Add Input port for task nodes
    public static IPort AddTaskInputPort(Node.IPortDefinitionContext context)
    {
        return context.AddInputPort(TaskInputPortName)
            .WithDisplayName(TaskInputPortDisplayName)
            .WithConnectorUI(PortConnectorUI.Circle)
            .Build();
    }
    
    // Add output port for task nodes
    public static IPort AddTaskOutputPort(Node.IPortDefinitionContext context)
    {
        return context.AddOutputPort(TaskOutputPortName)
            .WithDisplayName(TaskOutputPortDisplayName)
            .WithConnectorUI(PortConnectorUI.Circle)
            .Build();
    }
    
    // Get next Tasks from output port
    public static List<TaskSO> GetConnectedTasks(IPort taskPort)
    {
        var connectedPorts = new List<IPort>(); 
        taskPort.GetConnectedPorts(connectedPorts);
        return connectedPorts.Select(port => port.GetNode())
            .OfType<ITaskNode>()
            .Select(node => node.GetTask())
            .ToList();
    }
}
