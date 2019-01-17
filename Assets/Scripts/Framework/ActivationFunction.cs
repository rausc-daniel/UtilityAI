using System;
using UnityEngine;

public abstract class ActivationFunction
{
    protected float Scalar;

    protected ActivationFunction(float scalar)
    {
        Scalar = scalar;
    }
    
    public abstract float Evaluate(float x);
}

public class SigmoidFunction : ActivationFunction
{
    public override float Evaluate(float x) => 1 / (1 + (float) Math.Pow(Math.E, -Scalar * x));

    public SigmoidFunction(float scalar) : base(scalar)
    {
    }
}

public class InvertedSigmoidFunction : ActivationFunction
{
    public override float Evaluate(float x) => 1 / (1 + (float) Math.Pow(Math.E, -Scalar * -x));

    public InvertedSigmoidFunction(float scalar) : base(scalar)
    {
    }
}

public class Linear : ActivationFunction
{
    public override float Evaluate(float x) => Scalar * x + 0.5f;

    public Linear(float scalar) : base(scalar)
    {
    }
}

public class Step : ActivationFunction
{
    public override float Evaluate(float x) => x >= Scalar ? 1 : 0;

    public Step(float scalar) : base(scalar)
    {
    }
}

public class Base : ActivationFunction
{
    public override float Evaluate(float x) => x >= Scalar ? 1 : 0;

    public Base(float scalar = 0.2f) : base(scalar)
    {
    }
}