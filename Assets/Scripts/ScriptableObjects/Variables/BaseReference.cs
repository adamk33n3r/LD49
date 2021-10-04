[System.Serializable]
public class BaseReference<T, T2> where T : BaseVariable<T2>
{
    public T variable;
    public T2 constant;
    public bool useVariable = true;
    public T2 Value => useVariable ? variable.Value : constant;

    public static implicit operator T2(BaseReference<T, T2> reference) {
        return reference.Value;
    }
}
