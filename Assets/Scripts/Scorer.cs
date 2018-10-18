using System;
using UnityEngine;

public class Scorer : ScriptableObject
{
    [SerializeField] protected float score;

    public float Score => score;
    public int Id;

    protected AiClient MyClient = null;

    public virtual void Initialize(AiClient client) => MyClient = client;
}
