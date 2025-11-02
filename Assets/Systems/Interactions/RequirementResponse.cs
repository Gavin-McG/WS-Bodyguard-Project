using UnityEngine;

public class RequirementResponse : InteractionResponse
{
    RequirementSO requirement;

    protected override void TriggerResponse()
    {
        requirement.CompleteRequirement();
    }
}
