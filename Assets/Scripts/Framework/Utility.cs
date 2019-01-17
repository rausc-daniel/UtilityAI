using System;
using UnityEngine;

public class Utility
{
    private readonly Value<float> val;
    private readonly OnExecute onExecute;
    private readonly ActivationFunction activationFunction;

    public Utility(Value<float> val, OnExecute onExecute, ActivationFunction activationFunction)
    {
        this.val = val;
        this.onExecute = onExecute;
        this.activationFunction = activationFunction;
    }

    public float Evaluate()
    {
        var value = activationFunction.Evaluate(val);
        return value;
    }
    

    public void Execute() => onExecute();
}
