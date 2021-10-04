using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class PassiveInventory : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<PassiveAbility> initialPassives;
    [System.NonSerialized]
    public List<PassiveAbility> passives;
    public void OnAfterDeserialize()
    {
        this.passives = new List<PassiveAbility>(this.initialPassives);
    }

    public void OnBeforeSerialize()
    {
    }
}
