using UnityEngine;

[CreateAssetMenu(fileName = "NewFlee", menuName = "Actions/Flee")]
class Flee : Action
{
    public override float Evaluate()
    {
        return Scorers[0].Score;
    }

    public override void Execute()
    {
        Debug.Log($"Fleeing: {Scorers[0].Score}");
    }
}
