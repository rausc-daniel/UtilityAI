using UnityEngine;

[CreateAssetMenu(fileName = "NewFlee", menuName = "Actions/Flee")]
class Flee : Action
{
    private Hider myHider;
    
    public override void Initialize(AiClient client)
    {
        myHider = (Hider) client;
        base.Initialize(client);
    }

    public override float Evaluate()
    {
        return Scorers[0].Score;
    }

    public override void Execute()
    {
        myHider.Agent.speed = 6;
        myHider.Agent.SetDestination(-myHider.Follower.transform.position);
        base.Execute();
    }
}
