using UnityEngine;

[CreateAssetMenu(fileName = "NewWalk", menuName = "Actions/Walk")]
public class Walk : Action
{
    private Hider myHider;
    private Vector3 target = default(Vector3);

    public override unsafe void Initialize(AiClient client, float* animEval)
    {
        myHider = (Hider) client;
        target = myHider.Target.transform.position;
        base.Initialize(client, animEval);
    }

    public override void Execute()
    {
        if(target == default(Vector3)) return;
        myHider.Agent.speed = 3;
        myHider.Agent.SetDestination(target);
        base.Execute();
    }
}
