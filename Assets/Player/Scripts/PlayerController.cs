using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private SpriteResolver spriteResolver;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float walkFrameRate = 0.15f; // time per walking frame

    private Vector2 moveInput;
    private string currentLabel;

    private string rightLabel => "Right";
    private string leftLabel => "Left";
    private string forwardLabel => "Forward";
    private string backwardLabel => "Backward";

    private int walkFrame = 1;
    private float walkTimer;

    private void Update()
    {
        moveInput = moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        transform.position += move * (moveSpeed * Time.deltaTime);

        UpdateSpriteDirection(moveInput);
        UpdateCategory(moveInput);
        
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
            if (IsLabelValidForInput(currentLabel, input))
                return;

            if (input.x > 0) newLabel = rightLabel;
            else if (input.x < 0) newLabel = leftLabel;
        }
        else
        {
            if (input.x > 0) newLabel = rightLabel;
            else if (input.x < 0) newLabel = leftLabel;
            else if (input.y > 0) newLabel = backwardLabel;
            else if (input.y < 0) newLabel = forwardLabel;
        }

        if (!string.IsNullOrEmpty(newLabel) && newLabel != currentLabel)
        {
            currentLabel = newLabel;
            spriteResolver.SetCategoryAndLabel(spriteResolver.GetCategory(), currentLabel);
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

    private void UpdateCategory(Vector2 input)
    {
        if (input == Vector2.zero)
        {
            // Standing
            if (spriteResolver.GetCategory() != "Standing")
            {
                spriteResolver.SetCategoryAndLabel("Standing", currentLabel ?? forwardLabel);
                walkFrame = 1;
                walkTimer = 0f;
            }
        }
        else
        {
            // Walking → cycle frames
            walkTimer += Time.deltaTime;
            if (walkTimer >= walkFrameRate)
            {
                walkTimer = 0f;
                walkFrame++;
                if (walkFrame > 4) walkFrame = 1;
            }

            string walkingCategory = $"Walking-{walkFrame}";
            if (spriteResolver.GetCategory() != walkingCategory)
            {
                spriteResolver.SetCategoryAndLabel(walkingCategory, currentLabel ?? forwardLabel);
            }
        }
    }

    private void UpdateShaderKeywords()
    {
        Shader.SetGlobalVector("_PlayerPos", transform.position);
    }
}