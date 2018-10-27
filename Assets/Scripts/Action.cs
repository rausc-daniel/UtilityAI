using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public unsafe class Action : ScriptableObject
{
    [SerializeField] private AnimationCurve evalCurve;
    private float* animEval;
    protected AiClient MyClient;

    public virtual unsafe void Initialize(AiClient client, float* eval)
    {
        animEval = eval;
        MyClient = client;
    }

    public virtual float Evaluate()
    {
        return evalCurve.Evaluate(*animEval);
    }

    public virtual void Execute()
    {
        Debug.Log($"{MyClient.name}: {name.Substring(1)} with a score of {Evaluate()}");
    }
}