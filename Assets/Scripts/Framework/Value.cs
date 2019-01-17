using System;
using UnityEngine;

[Serializable]
public class Value<T>
{
    [SerializeField] private T val;
    private static Value<T> instance;
    public T Val => val;
    
    private Value(T val)
    {
        this.val = val;
        instance = this;
    }

    public static implicit operator Value<T>(T val)
    {
        if (instance == null)
            instance = new Value<T>(val);
        else
            instance.val = val;
        return instance;
    }
    public static implicit operator T(Value<T> val) => val.val;

    public override string ToString() => val.ToString();
}