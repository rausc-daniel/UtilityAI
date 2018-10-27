using UnityEngine;

[CreateAssetMenu(fileName = "NewFlee", menuName = "Actions/Flee")]
class Flee : Action
{
    private Hider myHider;
    
    public override unsafe void Initialize(AiClient client, float* animEval)
    {
        myHider = (Hider) client;
        base.Initialize(client, animEval);
    }

    public override void Execute()
    {
        myHider.Agent.speed = 6;
        myHider.Agent.SetDestination(-myHider.Follower.transform.position);
        base.Execute();
    }
}
