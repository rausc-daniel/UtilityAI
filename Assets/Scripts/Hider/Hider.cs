using System;
using EventSystem;
using UnityEngine;
using UnityEngine.AI;

public class Hider : AiClient
{
    public NavMeshAgent Agent;
    private bool hasBeenSpotted;
    public AiClient Follower;
    
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
                Follower = null;
            }
            EventManager.Instance.TriggerEvent(new Events.UtilityAi.OnValueChanged());
        });
    }
}
