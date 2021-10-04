using UnityEditor;

[CustomEditor(typeof(BoolVariable))]
public class BoolVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnityEngine.GUI.enabled = false;
        EditorGUILayout.Toggle("Runtime Value", (this.target as BoolVariable).Value);
        UnityEngine.GUI.enabled = true;
    }
}
