using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<T> InitialItems;
    [System.NonSerialized]
    public List<T> Items;

    public void OnAfterDeserialize()
    {
        this.Items = this.InitialItems;
    }

    public void OnBeforeSerialize()
    {
    }

    public void Add(T item)
    {
        if (!Items.Contains(item))
            Items.Add(item);
    }

    public void Remove(T item)
    {
        if (Items.Contains(item))
            Items.Remove(item);
    }
}
