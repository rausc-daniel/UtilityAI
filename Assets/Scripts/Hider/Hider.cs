using System;
using EventSystem;
using UnityEngine;
using UnityEngine.AI;

public class Hider : AiClient
{
    public PathNode Target;
    public NavMeshAgent Agent;
    public AiClient Follower;
    private float fleeEval;
    private float hideEval = 1f;
    private float walkEval = 1f;
    public Vector3 HidingTarget;
    
    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        EventManager.Instance.AddListener<Events.Senses.SpottingChanged>(e =>
        {
            if (Events.Senses.SpottingChanged.Target != null && Events.Senses.SpottingChanged.Target.Equals(gameObject))
            {
                fleeEval = 1;
                Follower = Events.Senses.SpottingChanged.Client;
            }
            else
            {
                fleeEval = 0;
                hideEval = 0f;
            }
            EventManager.Instance.TriggerEvent(new Events.UtilityAi.OnValueChanged());
        });
    }

    public override void UpdateClient()
    {
        hideEval += Time.deltaTime;
        base.UpdateClient();
    }

    public override unsafe void InitializeClient()
    {
        fixed (float* p1 = &fleeEval)
            Actions[0].Initialize(this, p1);
        fixed (float* p2 = &hideEval)
            Actions[1].Initialize(this, p2);
        fixed (float* p3 = &walkEval)
            Actions[2].Initialize(this, p3);
        
    }

    private void OnDrawGizmos()
    {
        if(HidingTarget != default(Vector3))
            Gizmos.DrawWireSphere(HidingTarget, 1);
    }
}
