using EventSystem;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewPatrol", menuName = "Actions/Follow")]
public class Follow : Action
{
    private Seeker mySeeker;
    
    public override void Initialize(AiClient client)
    {
        base.Initialize(client);
        mySeeker = (Seeker) MyClient;
    }

    public override float Evaluate() => Scorers[0].Score;

    public override void Execute()
    {
        if(mySeeker.CurrentTarget == null) return;
        mySeeker.Agent.SetDestination(mySeeker.CurrentTarget.transform.position);
        base.Execute();
    }
}
