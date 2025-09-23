using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private SpriteResolver spriteResolver;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;
    private string currentLabel;

    private string rightLabel => "Right";
    private string leftLabel => "Left";
    private string forwardLabel => "Forward";
    private string backwardLabel => "Backward";
    
    private void Update()
    {
        moveInput = moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        transform.position += move * (moveSpeed * Time.deltaTime);

        UpdateSpriteDirection(moveInput);
        
        UpdateShaderKeywords();
    }

    private void UpdateSpriteDirection(Vector2 input)
    {
        // Keep label unchanged when idle
        if (input == Vector2.zero)
            return; 

        string newLabel = null;

        // Prioritize horizontal when diagonal
        if (Mathf.Abs(input.x) > 0 && Mathf.Abs(input.y) > 0)
        {
            // Diagonal movement → keep label if valid
            if (IsLabelValidForInput(currentLabel, input))
                return;

            // If invalid, prefer horizontal direction
            if (input.x > 0) newLabel = rightLabel;
            else if (input.x < 0) newLabel = leftLabel;
        }
        else
        {
            // Single-axis movement
            if (input.x > 0) newLabel = rightLabel;
            else if (input.x < 0) newLabel = leftLabel;
            else if (input.y > 0) newLabel = backwardLabel;
            else if (input.y < 0) newLabel = forwardLabel;
        }

        if (!string.IsNullOrEmpty(newLabel) && newLabel != currentLabel)
        {
            string currentCategory = spriteResolver.GetCategory();
            currentLabel = newLabel;
            spriteResolver.SetCategoryAndLabel(currentCategory, currentLabel);
        }
    }

    private bool IsLabelValidForInput(string label, Vector2 input)
    {
        if (string.IsNullOrEmpty(label)) return false;

        if (input.x > 0 && label == rightLabel) return true;
        if (input.x < 0 && label == leftLabel) return true;
        if (input.y > 0 && label == backwardLabel) return true;
        if (input.y < 0 && label == forwardLabel) return true;

        return false;
    }

    private void UpdateShaderKeywords()
    {
        Shader.SetGlobalVector("_PlayerPos", transform.position);
    }
}
