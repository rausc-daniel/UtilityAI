using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Action : ScriptableObject
{
    [SerializeField] public string Name;
    
    // Influencing Factors
    [SerializeField] public List<Scorer> Scorers;
    protected AiClient MyClient;

    // Constructor
    public virtual void Initialize(AiClient client) => MyClient = client;

    // Advantage, that this Utility has for the Client
    public virtual float Evaluate()
    {
        return Scorers[0].Score;
    }

    // Execute this Utility
    public virtual void Execute()
    {
        Debug.Log($"{MyClient.name}: {name.Substring(1)} with Score of {Evaluate()}");
    }
}
