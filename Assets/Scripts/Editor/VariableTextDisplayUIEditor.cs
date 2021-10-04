using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(VariableTextDisplayUI))]
public class VariableTextDisplayUIEditor : Editor
{
    private SerializedProperty text;
    private SerializedProperty preText;
    private SerializedProperty variableType;
    private SerializedProperty intRef;
    private SerializedProperty floatRef;

    public void OnEnable()
    {
        this.text = this.serializedObject.FindProperty("text");
        this.preText = this.serializedObject.FindProperty("preText");
        this.variableType = this.serializedObject.FindProperty("variableType");
        this.intRef = this.serializedObject.FindProperty("intRef");
        this.floatRef = this.serializedObject.FindProperty("floatRef");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(this.text);
        EditorGUILayout.PropertyField(this.preText);
        EditorGUILayout.PropertyField(this.variableType);
        var variableType = (VariableTextDisplayUI.VariableType)System.Enum.Parse(typeof(VariableTextDisplayUI.VariableType), this.variableType.enumNames[this.variableType.enumValueIndex]);
        switch (variableType) {
            case VariableTextDisplayUI.VariableType.Integer:
                EditorGUILayout.PropertyField(this.intRef);
                break;
            case VariableTextDisplayUI.VariableType.Float:
                EditorGUILayout.PropertyField(this.floatRef);
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
