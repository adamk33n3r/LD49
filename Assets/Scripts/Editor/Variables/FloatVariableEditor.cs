using UnityEditor;

[CustomEditor(typeof(FloatVariable))]
public class FloatVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnityEngine.GUI.enabled = false;
        EditorGUILayout.FloatField("Runtime Value", (this.target as FloatVariable).Value);
        UnityEngine.GUI.enabled = true;
    }
}
