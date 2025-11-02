using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EventSO", menuName = "Task System/Requirement")]
public class RequirementSO : ScriptableObject
{
    [HideInInspector, SerializeField]
    public UnityEvent OnRequirementCompleted = new UnityEvent();
    
    public void CompleteRequirement() =>
        OnRequirementCompleted.Invoke();
}
