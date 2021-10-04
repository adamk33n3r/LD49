using UnityEditor;

[CustomEditor(typeof(AbilityRuntimeSet))]
public class AbilityRuntimeSetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnityEngine.GUI.enabled = false;
        var items = (this.target as AbilityRuntimeSet).Items;
        EditorGUILayout.IntField("Runtime Value", items.Count);
        foreach (var item in items) {
            EditorGUILayout.ObjectField(item, typeof(Ability), false);
        }
        UnityEngine.GUI.enabled = true;
    }
}
