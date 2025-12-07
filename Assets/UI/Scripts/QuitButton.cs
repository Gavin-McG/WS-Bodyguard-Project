using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
