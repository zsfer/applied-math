using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(RangeDetector))]
public class RangeDetectorEditor : Editor
{
    private void OnSceneGUI()
    {
        var t = target as RangeDetector;

        Handles.color = t.IsInRange ? Color.green : Color.red;
        Handles.DrawWireDisc(t.transform.position, Vector3.up, t.Range, 2);

        Handles.Label(t.Center, $"Distance: {t.Distance}");
    }
}
