using UnityEditor;

[CustomEditor(typeof(PassiveAbilityRuntimeSet))]
public class PassiveAbilityRuntimeSetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnityEngine.GUI.enabled = false;
        var items = (this.target as PassiveAbilityRuntimeSet).Items;
        EditorGUILayout.IntField("Runtime Value", items.Count);
        foreach (var item in items) {
            EditorGUILayout.ObjectField(item, typeof(PassiveAbility), false);
        }
        UnityEngine.GUI.enabled = true;
    }
}
