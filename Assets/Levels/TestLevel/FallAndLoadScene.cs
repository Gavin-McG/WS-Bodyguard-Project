using UnityEngine;
using UnityEngine.SceneManagement;
using WolverineSoft.DialogueSystem;

public class FallAndLoadScene : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private DSEvent evnt;

    [Header("Fall Settings")]
    [SerializeField] private float gravitySpeed = 5f;

    [Header("Scene Transition")]
    [SerializeField] private float delayBeforeSceneLoad = 2f;
    [SerializeField] private string sceneToLoad;

    private bool isFalling = false;

    private void OnEnable()
    {
        if (evnt != null)
            evnt.AddListener(OnEventTriggered);
    }

    private void OnDisable()
    {
        if (evnt != null)
            evnt.RemoveListener(OnEventTriggered);
    }

    private void OnEventTriggered()
    {
        if (!isFalling)
            StartCoroutine(FallAndLoad());
    }

    private System.Collections.IEnumerator FallAndLoad()
    {
        isFalling = true;

        float timer = 0f;

        while (timer < delayBeforeSceneLoad)
        {
            // Simple gravity movement
            transform.position += Vector3.down * (gravitySpeed * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        // Load scene after falling for the delay duration
        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
    }
}
