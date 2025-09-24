using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private SpriteResolver spriteResolver;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float walkFrameRate = 0.15f; // time per walking frame
    [SerializeField] private float maxStepSize = 0.26f;
    [SerializeField] private LayerMask collisionMask = ~0; // everything by default

    private Vector2 moveInput;
    private string currentLabel;

    private string rightLabel => "Right";
    private string leftLabel => "Left";
    private string forwardLabel => "Forward";
    private string backwardLabel => "Backward";

    private int walkFrame = 1;
    private float walkTimer;

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        moveInput = moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        if (move.sqrMagnitude > 0f)
        {
            TryMoveWithStep(move.normalized * (moveSpeed * Time.deltaTime));
        }

        UpdateSpriteDirection(moveInput);
        UpdateCategory(moveInput);
        UpdateShaderKeywords();
    }

    private void TryMoveWithStep(Vector3 move)
    {
        Vector3 pos = transform.position;
        Quaternion orientation = transform.rotation;

        // Collider info
        Vector3 fullSize = Vector3.Scale(boxCollider.size, transform.lossyScale);
        float playerHeight = fullSize.y;
        float playerWidth = fullSize.x;
        float playerLength = fullSize.z;

        float smallPadding = 0.02f;   // shrink sides to avoid catching walls
        float smallHeight = 0.05f;    // thin slice
        float stepHeight = maxStepSize;

        // Proposed new position if we just moved
        Vector3 targetPos = pos + move;

        // Start the cast a little above the player's head at target position
        Vector3 castCenter = targetPos + Vector3.up * (playerHeight * 0.5f + stepHeight);
        Vector3 halfExtents = new Vector3(
            (playerWidth * 0.5f) - smallPadding,
            smallHeight * 0.5f,
            (playerLength * 0.5f) - smallPadding
        );

        // Cast downward to find floor
        if (Physics.BoxCast(castCenter, halfExtents, Vector3.down,
                out RaycastHit hit, orientation,
                playerHeight + stepHeight * 2f, collisionMask))
        {
            // The distance from top to hit
            float hitDist = hit.distance;

            // Expected player bottom height (measured from cast origin)
            float expectedBottom = playerHeight + stepHeight;

            // Check if hit point is within valid stepping range
            float diff = expectedBottom - hitDist;
            if (Mathf.Abs(diff) <= stepHeight)
            {
                // Snap to floor (subtract distance from cast start to hit, then add player half-height)
                float newY = castCenter.y - hitDist + playerHeight * 0.5f;
                targetPos.y = newY;
                transform.position = targetPos;
                return;
            }
        }

        // Fallback if no floor found
        transform.position = targetPos;
    }


    private void UpdateSpriteDirection(Vector2 input)
    {
        if (input == Vector2.zero)
            return;

        string newLabel = null;

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
            if (spriteResolver.GetCategory() != "Standing")
            {
                spriteResolver.SetCategoryAndLabel("Standing", currentLabel ?? forwardLabel);
                walkFrame = 1;
                walkTimer = 0f;
            }
        }
        else
        {
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
