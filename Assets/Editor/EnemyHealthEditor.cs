using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyHealth))]
public class EnemyHealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemyHealth enemyHealth = (EnemyHealth)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Damage Enemy"))
        {
            enemyHealth.Damage();
        }
    }
}
