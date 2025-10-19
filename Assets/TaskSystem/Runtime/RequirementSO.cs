using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EventSO", menuName = "Task System/Requirement")]
public class RequirementSO : ScriptableObject
{
    [HideInInspector, SerializeField]
    private UnityEvent completeEvent = new UnityEvent();
    
    public void CompleteRequirement() =>
        completeEvent.Invoke();

    public void AddCompleteListener(UnityAction call) =>
        completeEvent.AddListener(call);

    public void RemoveCompleteListener(UnityAction call) =>
        completeEvent.RemoveListener(call);
    
    public void RemoveAllListeners() =>
        completeEvent.RemoveAllListeners();
}
