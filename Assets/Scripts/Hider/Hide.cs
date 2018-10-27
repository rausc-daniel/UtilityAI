using EventSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHide", menuName = "Actions/HideBehindObstacle")]
class Hide : Action
{
    private Collider[] obstacles;
    private Hider myHider;
    private Vector3 hidingSpot;
    private Vector3 seekerPos;

    public override void Initialize(AiClient client)
    {
        obstacles = Physics.OverlapSphere(client.transform.position, int.MaxValue, 1 << 13);
        myHider = (Hider)client;

        EventManager.Instance.AddListener<Events.UtilityAi.OnActionChanged>(e =>
        {
            if(Events.UtilityAi.OnActionChanged.Origin.Equals(myHider) && Events.UtilityAi.OnActionChanged.Action.GetType() == GetType())
                HideBehindObstacle(FindClosest(myHider.transform.position));
            else if (Events.UtilityAi.OnActionChanged.Origin.Equals(myHider) &&
                     Events.UtilityAi.OnActionChanged.Action.GetType() != GetType())
                myHider.hidingTarget = default(Vector3);
        });
        base.Initialize(client);
    }

    public override float Evaluate()
    {
        DecliningScorer scorer = (DecliningScorer)Scorers[0];
        scorer.DeclineScore();
        return Scorers[0].Score;
    }

    private void HideBehindObstacle(Collider col)
    {
        if(myHider.Follower == null) return;
        seekerPos = myHider.Follower.transform.position;
        Vector3 hidingDirection = col.transform.position - seekerPos;
        hidingDirection /= hidingDirection.magnitude;
        Ray ray = new Ray(col.transform.position, hidingDirection);
        float dist;
        col.bounds.IntersectRay(ray, out dist);
        hidingSpot = col.transform.position + (-hidingDirection * (dist + 0.5f));
        myHider.Agent.SetDestination(hidingSpot);
        myHider.hidingTarget = hidingSpot;
    }

    public override void Execute()
    {
        Debug.DrawLine(seekerPos, hidingSpot, Color.red);
        base.Execute();
    }

    private Collider FindClosest(Vector3 ownerPos)
    {
        if (obstacles.Length == 0) return null;

        Collider closest = obstacles[0];
        float smallestDistance = Vector3.Distance(ownerPos, closest.transform.position);

        for (int i = 1; i < obstacles.Length; i++)
        {
            Collider obj = obstacles[i];
            float dist = Vector3.Distance(ownerPos, obj.transform.position);

            if (!(dist < smallestDistance)) continue;

            smallestDistance = dist;
            closest = obj;
        }

        return closest;
    }
}
