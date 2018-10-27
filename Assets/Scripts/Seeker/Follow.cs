using EventSystem;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewPatrol", menuName = "Actions/Follow")]
public class Follow : Action
{
    private Seeker mySeeker;
    
    public override unsafe void Initialize(AiClient client, float* animEval)
    {
        base.Initialize(client, animEval);
        mySeeker = (Seeker) MyClient;
    }

    public override void Execute()
    {
        if(mySeeker.CurrentTarget == null) return;
        mySeeker.Agent.SetDestination(mySeeker.CurrentTarget.transform.position);
        base.Execute();
    }
}
