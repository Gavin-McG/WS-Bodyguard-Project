using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEngine;

public class TaskOptions
{
    public INodeOption nameOption;
    public INodeOption descriptionOption;
    
    public static TaskOptions AddOptions(Node.IOptionDefinitionContext context)
    {
        TaskOptions taskOptions = new TaskOptions();
        taskOptions.nameOption = context.AddOption("Name", typeof(string)).Build();
        taskOptions.descriptionOption = context.AddOption("Description", typeof(string)).Build();
        return taskOptions;
    }

    public TaskSO GetTask()
    {
        TaskSO task = ScriptableObject.CreateInstance<TaskSO>();
        nameOption.TryGetValue(out string name);
        task.name = name;
        
        descriptionOption.TryGetValue(out task.description);
        task.requirements = new List<TaskSO.Requirement>();
        task.nextTasks = new List<TaskSO>();
        
        return task;
    }
}
