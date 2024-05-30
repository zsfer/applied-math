using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RangeDetector : MonoBehaviour
{
    public bool IsInRange { get; private set; } = false;

    [field: SerializeField] public float Range { get; private set; } = 5;
    [SerializeField] Transform _target;

    // to get magnitude: sqrt((a.x - b.x)^2 + (a.y - b.y)^2)
    public float Distance => (_target.position - transform.position).magnitude;
    public Vector3 Center => (_target.position - transform.position) / 2;

    private void OnDrawGizmos()
    {
        IsInRange = Distance <= Range;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, _target.position);
    }
}
