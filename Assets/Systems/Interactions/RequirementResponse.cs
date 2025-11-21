using UnityEngine;

public class RequirementResponse : InteractionResponse
{
    [SerializeField] private RequirementSO requirement;

    protected override void TriggerResponse()
    {
        requirement.CompleteRequirement();
    }
}
