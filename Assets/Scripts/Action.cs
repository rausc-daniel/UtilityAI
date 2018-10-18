using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Action : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public List<Scorer> Scorers;
    protected AiClient MyClient;

    public virtual void Initialize(AiClient client) => MyClient = client;

    public virtual float Evaluate()
    {
        throw new NotImplementedException("Implement Evaluate() in child class");
    }

    public virtual void Execute()
    {
        Debug.Log($"{name}: with Score of: {Evaluate()}");
    }
}