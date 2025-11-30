using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WolverineSoft.DialogueSystem.Default;

namespace WolverineSoft.DialogueSystem.DefaultUI
{
    public class ProfileUI : MonoBehaviour
    {
        [SerializeField] GameObject profileUI;
        [SerializeField] Image profileImage;
        [SerializeField] GameObject profileTextBox;
        [SerializeField] TextMeshProUGUI profileText;

        public void Disable()
        {
            profileUI.SetActive(false);
        }
        
        public void SetProfile(DialogueProfile profile)
        {
            profileUI.SetActive(true);
            
            profileImage.gameObject.SetActive(profile.sprite!=null);
            profileImage.sprite = profile.sprite;
            
            profileTextBox.SetActive(profile.characterName!="");
            profileText.text = profile.characterName;
        }
    }
}