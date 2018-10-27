using System;
using EventSystem;
using UnityEngine;
using UnityEngine.AI;

public class Hider : AiClient
{
    public PathNode Target;
    public NavMeshAgent Agent;
    private bool hasBeenSpotted;
    public AiClient Follower;
    public Vector3 hidingTarget;
    
    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        EventManager.Instance.AddListener<Events.Senses.SpottingChanged>(e =>
        {
            if (Events.Senses.SpottingChanged.Target != null && Events.Senses.SpottingChanged.Target.Equals(gameObject))
            {
                hasBeenSpotted = true;
                Follower = Events.Senses.SpottingChanged.Client;
            }
            else
            {
                hasBeenSpotted = false;
                // Follower = null;
            }
            EventManager.Instance.TriggerEvent(new Events.UtilityAi.OnValueChanged());
        });
    }

    private void OnDrawGizmos()
    {
        if(hidingTarget != default(Vector3))
            Gizmos.DrawWireSphere(hidingTarget, 1);
    }
}
