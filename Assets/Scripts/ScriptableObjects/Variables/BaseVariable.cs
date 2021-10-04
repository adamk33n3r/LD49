using UnityEngine;

public class BaseVariable<T> : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private T InitialValue;
    [System.NonSerialized]
    public T Value;

    public void OnAfterDeserialize()
    {
        Value = InitialValue;
    }

    public void OnBeforeSerialize()
    {
    }

    public static implicit operator T(BaseVariable<T> reference) {
        return reference.Value;
    }
}
