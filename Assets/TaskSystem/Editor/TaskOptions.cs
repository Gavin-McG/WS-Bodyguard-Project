using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEngine;

public class TaskOptions
{
    public INodeOption nameOption;
    public INodeOption descriptionOption;
    public INodeOption requirePrevious;
    
    public static TaskOptions AddOptions(Node.IOptionDefinitionContext context, bool includePrevious = true)
    {
        TaskOptions taskOptions = new TaskOptions();
        taskOptions.nameOption = context.AddOption("Name", typeof(string)).Build();
        taskOptions.descriptionOption = context.AddOption("Description", typeof(string)).Build();
        taskOptions.requirePrevious = includePrevious ? context.AddOption("Require Previous", typeof(bool)).Build() : null;
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
        task.previousTasks = new List<TaskSO>();
        requirePrevious?.TryGetValue(out task.requirePrevious);
        
        return task;
    }
}
