using System;
using UnityEngine;

public class EditorDebugSphere : MonoBehaviour
{
    [SerializeField] Color color  = Color.red;
    [SerializeField] private float radius = 0.25f;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
