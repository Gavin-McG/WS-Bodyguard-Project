using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    [SerializeField] public float rectHeight = 30;
    [SerializeField] RectTransform taskTransform;
    [SerializeField] Image bulletImage;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] SpriteLibraryAsset bulletLibrary;

    [Space(10)]
    [SerializeField] Transform requirementList;    
    [SerializeField] RequirementUI requirementPrefab;
    
    private List<RequirementUI> requirements = new();
    
    private const string SpriteCategory = "Task";

    public void Init(TaskManager.TaskInfo info)
    {
        float newHeight = rectHeight + requirementPrefab.rectHeight*info.requirements.Count;
        Vector2 newMinAnchor = new Vector2(taskTransform.anchorMin.x, taskTransform.anchorMax.y - newHeight);
        taskTransform.anchorMin = newMinAnchor;
        
        SetSprite("Base");
        description.text = info.description;

        foreach (var requirement in info.requirements)
        {
            RequirementUI requirementUI = Instantiate<RequirementUI>(requirementPrefab, requirementList);
            requirementUI.Init(requirement);
            requirements.Add(requirementUI);
        }
    }

    private void SetSprite(string Label)
    {
        bulletImage.sprite = bulletLibrary.GetSprite(SpriteCategory, Label);
    }

    public void SucceedTask()
    {
        SetSprite("Success");
        foreach (var requirement in requirements)
        {
            requirement.SucceedRequirement();
        }
        //TODO remove task
        
    }

    public void FailedTask()
    {
        SetSprite("Fail");
        foreach (var requirement in requirements)
        {
            requirement.FailedRequirement();
        }
        //TODO remove task
    }

    public void SucceedRequirement(TaskManager.RequirementEventData data)
    {
        requirements[data.requirementIndex].SucceedRequirement();
    }

    public void FailedRequirement(TaskManager.RequirementEventData data)
    {
        requirements[data.requirementIndex].FailedRequirement();
    }
}