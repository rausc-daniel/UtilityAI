using UnityEngine;

[CreateAssetMenu(fileName = "NewPatrol", menuName = "Actions/Patrol")]
public class Patrol : Action
{
    private Seeker seeker;
    private Vector3 currentTarget = default(Vector3);

    public override float Evaluate() => Scorers[0].Score;

    public override void Execute()
    {
        Debug.Log($"Patrolling: {Scorers[0].Score}");
        if (seeker == null) seeker = (Seeker) MyClient;
        if (currentTarget == default(Vector3)) currentTarget = seeker.Path[0].gameObject.transform.position;
        if (Vector3.Distance(seeker.transform.position, currentTarget) < 1)
            currentTarget = seeker.Path[Random.Range(0, seeker.Path.Count)].transform.position;
        seeker.Agent.SetDestination(currentTarget);
    }


}