using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<Ability> initialAbilities;
    [System.NonSerialized]
    public List<Ability> abilities;
    public void OnAfterDeserialize()
    {
        this.abilities = new List<Ability>(this.initialAbilities);
    }

    public void OnBeforeSerialize()
    {
    }
}
