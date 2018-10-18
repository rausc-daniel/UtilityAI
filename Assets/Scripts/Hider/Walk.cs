using UnityEngine;

[CreateAssetMenu(fileName = "NewWalk", menuName = "Actions/Walk")]
public class Walk : Action
{
    [SerializeField] private GameObject target;
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
        myHider.Agent.SetDestination(target.transform.position);
        base.Execute();
    }
}
