using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

[EditorTool("Bounding Box Tool", typeof(Transform))]
public class BoundingBoxTool : EditorTool
{
    private GUIContent _iconContent;

	private const float increment = 0.25f;

    private void OnEnable()
    {
        _iconContent = new GUIContent()
        {
            image = EditorGUIUtility.IconContent("RectTool On").image,
            text = "Bounding Box Tool",
            tooltip = "Manipulate object bounding box with handles"
        };
    }

    public override GUIContent toolbarIcon => _iconContent;

    public override void OnToolGUI(EditorWindow window)
    {
        if (!(target is Transform t))
            return;

        // Reset rotation so we only work with axis-aligned bounding boxes
        if (t.rotation != Quaternion.identity)
            t.rotation = Quaternion.identity;

        var renderer = t.GetComponent<Renderer>();
        if (renderer == null)
            return;

        Bounds bounds = renderer.bounds;

        // Centers of the 6 bounding box faces
        Vector3[] normals = new Vector3[]
        {
            Vector3.left, Vector3.right,
            Vector3.down, Vector3.up,
            Vector3.back, Vector3.forward
        };

        EditorGUI.BeginChangeCheck();

        for (int i = 0; i < normals.Length; i++)
        {
            Vector3 normal = normals[i];
            Vector3 handlePos = bounds.center + Vector3.Scale(bounds.extents, normal);

            // Draw slider handle
            Vector3 newHandlePos = Handles.Slider(handlePos, normal, HandleUtility.GetHandleSize(handlePos) * 0.1f, Handles.CubeHandleCap, 0f);

            if (newHandlePos != handlePos) // only reacts when the handle is moved
            {
                Undo.RecordObject(t, "Resize Bounding Box");

                // Snap movement to whole units
                float delta = Vector3.Dot(newHandlePos - handlePos, normal);
                delta = Mathf.Round(delta/increment)*increment;

                if (Mathf.Abs(delta) > 0f)
                {
                    Vector3 scaleChange = Vector3.zero;
                    Vector3 positionChange = Vector3.zero;

                    // Scale change is proportional to delta
                    if (normal == Vector3.left || normal == Vector3.right)
                    {
                        scaleChange.x = delta;
                        positionChange.x = delta * 0.5f * (normal == Vector3.left ? -1 : 1);
                    }
                    else if (normal == Vector3.down || normal == Vector3.up)
                    {
                        scaleChange.y = delta;
                        positionChange.y = delta * 0.5f * (normal == Vector3.down ? -1 : 1);
                    }
                    else if (normal == Vector3.back || normal == Vector3.forward)
                    {
                        scaleChange.z = delta;
                        positionChange.z = delta * 0.5f * (normal == Vector3.back ? -1 : 1);
                    }

                    // Apply new scale and position
                    t.localScale += scaleChange;
                    t.position += positionChange;
                }
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(t);
        }
    }
}
