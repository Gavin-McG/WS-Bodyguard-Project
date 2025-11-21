using System;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform target;
    
    private Vector3 offset;

    private void OnEnable()
    {
        offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = target.position + offset;
    }
}
