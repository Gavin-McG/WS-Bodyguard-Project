using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.GraphToolkit.Editor;

public class RequirmentOptions
{
    public INodeOption descriptionOption;
    public INodeOption eventOption;
    
    public static RequirmentOptions AddOptions(Node.IOptionDefinitionContext context)
    {
        RequirmentOptions requirementOptions = new RequirmentOptions();
        requirementOptions.descriptionOption = context.AddOption("Description", typeof(string)).Build();
        requirementOptions.eventOption = context.AddOption("Event", typeof(EventSO)).Build();
        return requirementOptions;
    }

    public TaskSO.Requirement GetRequirement()
    {
        TaskSO.Requirement requirement = new TaskSO.Requirement();
        descriptionOption.TryGetValue(out requirement.description);
        eventOption.TryGetValue(out requirement.eventSO);
        
        return requirement;
    }
}