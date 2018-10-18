using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : AiClient
{
    [SerializeField] public List<PathNode> Path;
    [SerializeField] private int range;
    [SerializeField, Range(0, 360)] private int spread;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private LayerMask obstacleLayer;

    public NavMeshAgent Agent;
    private OverlapSphereVision vision;
    public GameObject CurrentTarget;
    private GameObject prevTarget;
    private List<Collider> hitTargets = new List<Collider>();
    private List<Collider> prevHitTargets = new List<Collider>();

    private bool hasSpotted;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        vision = new OverlapSphereVision(this, range, hitLayer);
        EventManager.Instance.AddListener<Events.Senses.SpottingChanged>(
            e =>
            {
                if (Events.Senses.SpottingChanged.Client.Equals(this))
                    CurrentTarget = Events.Senses.SpottingChanged.Target;
                if (CurrentTarget == null)
                    hasSpotted = false;
                if (CurrentTarget != null)
                    hasSpotted = true;
                Debug.Log(hasSpotted);
                EventManager.Instance.TriggerEvent(new Events.UtilityAi.OnValueChanged());
            });
        CoroutineHelper.Instance.RunCoroutine(HandleVision(), "SeekerVision");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        foreach (var pathNode in Path)
            Gizmos.DrawWireSphere(pathNode.transform.position, 1);
    }

    private IEnumerator HandleVision()
    {
        while (true)
        {
            hitTargets = vision.QueryVision().ToList();

            GameObject target = FindClosest(transform.position);

            if (prevHitTargets.Count == 0 && hitTargets.Count > 0)
                EventManager.Instance.TriggerEvent(new Events.Senses.SpottingChanged(this, target));
            else if (prevHitTargets.Count > 0 && hitTargets.Count == 0)
                EventManager.Instance.TriggerEvent(new Events.Senses.SpottingChanged(this, target));
            else if (prevHitTargets.Count > 0 && hitTargets.Count > 0 && !prevTarget.Equals(target))
                EventManager.Instance.TriggerEvent(new Events.Senses.SpottingChanged(this, target));

            prevHitTargets = hitTargets.Copy();

            prevTarget = target;

            yield return null;
        }
    }

    private GameObject FindClosest(Vector3 ownerPos)
    {
        hitTargets = FindVisibleTargets(ownerPos);

        if (hitTargets.Count == 0) return null;

        GameObject closest = hitTargets[0].gameObject;
        float smallestDistance = Vector3.Distance(ownerPos, closest.transform.position);

        for (int i = 1; i < hitTargets.Count; i++)
        {
            Collider obj = hitTargets[i];
            float dist = Vector3.Distance(ownerPos, obj.transform.position);

            if (!(dist < smallestDistance)) continue;

            smallestDistance = dist;
            closest = obj.gameObject;
        }

        return closest;
    }

    private List<Collider> FindVisibleTargets(Vector3 ownerPos)
    {
        List<Collider> tmp = new List<Collider>();

        foreach (Collider hit in hitTargets)
        {
            if (Vector3.Angle(ownerPos, hit.transform.position) <= (float) spread / 2)
                tmp.Add(hit);
            if (!Physics.Raycast(new Ray(ownerPos, hit.transform.position), range, obstacleLayer))
                tmp.Add(hit);
        }

        return tmp;
    }
}