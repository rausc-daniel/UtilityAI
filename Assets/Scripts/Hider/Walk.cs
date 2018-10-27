using UnityEngine;

[CreateAssetMenu(fileName = "NewWalk", menuName = "Actions/Walk")]
public class Walk : Action
{
    private Hider myHider;
    private Vector3 target = default(Vector3);

    public override void Initialize(AiClient client)
    {
        myHider = (Hider) client;
        target = myHider.Target.transform.position;
        base.Initialize(client);
    }

    public override float Evaluate()
    {
        return Scorers[0].Score;
    }

    public override void Execute()
    {
        if(target == default(Vector3)) return;
        myHider.Agent.speed = 3;
        myHider.Agent.SetDestination(target);
        base.Execute();
    }
}
