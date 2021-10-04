using UnityEditor;

[CustomEditor(typeof(GameObjectRuntimeSet))]
public class RuntimeSetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnityEngine.GUI.enabled = false;
        var items = (this.target as GameObjectRuntimeSet).Items;
        EditorGUILayout.IntField("Runtime Value", items.Count);
        foreach (var item in items) {
            EditorGUILayout.ObjectField(item, typeof(UnityEngine.GameObject), false);
        }
        UnityEngine.GUI.enabled = true;
    }
}
