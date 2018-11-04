using System;
using UnityEngine;

public class Scorer : ScriptableObject
{
    // Influence
    [SerializeField] protected float score;

    public float Score
    {
        get { return score; }
    }


    // Owner
    protected AiClient MyClient;

    // Constructor
    public virtual void Initialize(AiClient client)
    {
        MyClient = client;
    }
}
