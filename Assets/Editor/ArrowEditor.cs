using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Arrow))]
public class ArrowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Arrow arrow = (Arrow)target;

        // Add a button to trigger the Shoot method
        if (GUILayout.Button("Shoot"))
        {
            arrow.Shoot();
        }
    }
}
