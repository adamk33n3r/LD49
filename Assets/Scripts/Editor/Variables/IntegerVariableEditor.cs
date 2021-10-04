using UnityEditor;

[CustomEditor(typeof(IntegerVariable))]
public class IntegerVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnityEngine.GUI.enabled = false;
        EditorGUILayout.IntField("Runtime Value", (this.target as IntegerVariable).Value);
        UnityEngine.GUI.enabled = true;
    }
}
