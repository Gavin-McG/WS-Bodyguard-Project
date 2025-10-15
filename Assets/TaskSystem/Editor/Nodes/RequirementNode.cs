using System;
using Unity.GraphToolkit.Editor;

[UseWithContext(typeof(TaskNode)), Serializable]
public class RequirementNode : BlockNode
{
    protected RequirmentOptions requirementOptions;
    
    protected override void OnDefineOptions(IOptionDefinitionContext context)
    {
        requirementOptions = RequirmentOptions.AddOptions(context);
    }

    public TaskSO.Requirement GetRequirement()
    {
        return requirementOptions.GetRequirement();
    }
}