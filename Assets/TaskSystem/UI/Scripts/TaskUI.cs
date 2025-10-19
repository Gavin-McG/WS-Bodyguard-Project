using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    [SerializeField] Image bulletImage;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] SpriteLibraryAsset bulletLibrary;

    [Space(10)]
    [SerializeField] GameObject requirementList;    
    [SerializeField] RequirementUI requirementPrefab;
    
    private List<RequirementUI> requirements;
    
    private const string SpriteCategory = "Task";

    public void Init(TaskManager.TaskInfo info)
    {
        SetSprite("Base");
        description.text = info.description;

        foreach (var requirement in info.requirements)
        {
            RequirementUI requirementUI = Instantiate<RequirementUI>(requirementPrefab);
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
    }

    public void FailedTask()
    {
        SetSprite("Fail");
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