using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : AiClient
{
    [SerializeField] public List<PathNode> Path;
    [SerializeField] private int range;
    [SerializeField, Range(0, 360)] private int spread;

    public NavMeshAgent Agent;
    private OverlapSphereVision vision;
    public GameObject CurrentTarget;

    private bool hasSpotted;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        vision = new OverlapSphereVision(this, spread, range, 1 << 12, 1 << 13);
        EventManager.Instance.AddListener<Events.Senses.SpottingChanged>(
            e =>
            {
                if(!Events.Senses.SpottingChanged.Client.Equals(this))
                CurrentTarget = Events.Senses.SpottingChanged.Target;
                if (CurrentTarget == null)
                    hasSpotted = false;
                if (CurrentTarget != null)
                    hasSpotted = true;
                EventManager.Instance.TriggerEvent(new Events.UtilityAi.OnValueChanged());
            });
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        foreach (var pathNode in Path)
            Gizmos.DrawWireSphere(pathNode.transform.position, 1);
    }
}