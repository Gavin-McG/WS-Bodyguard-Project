using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class RequirementUI : MonoBehaviour
{
    [SerializeField] Image bulletImage;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] SpriteLibraryAsset bulletLibrary;
    
    private const string SpriteCategory = "Requirement";

    public void Init(TaskManager.RequirementInfo requirementInfo)
    {
        string spriteLabel = requirementInfo.completed ? "Success" : "Base";
        SetSprite(spriteLabel);
        description.text = requirementInfo.description;
    }
    
    private void SetSprite(string Label)
    {
        bulletImage.sprite = bulletLibrary.GetSprite(SpriteCategory, Label);
    }

    public void SucceedRequirement()
    {
        SetSprite("Success");
    }

    public void FailedRequirement()
    {
        SetSprite("Fail");
    }
}
