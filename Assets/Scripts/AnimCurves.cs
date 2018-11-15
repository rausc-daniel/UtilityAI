using UnityEngine;

public class AnimCurves : ScriptableObject
{
    public AnimCurves Instance;

    public AnimationCurve Step;
    public AnimationCurve LinearDown;
    public AnimationCurve Baseline;

    private void OnEnable()
    {
        Instance = this;
    }
}
