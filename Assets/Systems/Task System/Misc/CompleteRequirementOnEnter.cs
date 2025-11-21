using System;
using UnityEngine;

public class CompleteRequirementOnEnter : MonoBehaviour
{
    [SerializeField] RequirementSO requirement;

    private void OnTriggerEnter(Collider other)
    {
        requirement.CompleteRequirement();
    }
}
