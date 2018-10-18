using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class OverlapSphereVision
{
    private readonly MonoBehaviour owner;
    private readonly float angle;
    private readonly float range;
    private readonly LayerMask hitLayer;
    private readonly LayerMask obstacleLayer;
    private List<Collider> hitTargets;
    private List<Collider> prevHitTargets;
    private GameObject target;
    private GameObject prevTarget;

    public OverlapSphereVision(MonoBehaviour owner, float angle, float range, LayerMask hitLayer, LayerMask obstacleLayer)
    {
        this.owner = owner;
        this.angle = angle;
        this.range = range;
        this.hitLayer = hitLayer;
        this.obstacleLayer = obstacleLayer;

        CoroutineHelper.Instance.RunCoroutine(QueryVision(), "seekerVision");
    }

    private IEnumerator QueryVision()
    {
        while (true)
        {
            target = FindClosest(owner.transform.position);

            if (prevHitTargets != null && prevTarget != null)
            {
                if (prevHitTargets.Count == 0 && hitTargets.Count != 0)
                    EventManager.Instance.TriggerEvent(new Events.Senses.SpottingChanged((AiClient)owner, target));
                else if (prevHitTargets.Count > 0 && hitTargets.Count == 0)
                    EventManager.Instance.TriggerEvent(new Events.Senses.SpottingChanged((AiClient)owner, target));
                else if(prevHitTargets.Count > 0 && hitTargets.Count > 0 && !prevTarget.Equals(target))
                    EventManager.Instance.TriggerEvent(new Events.Senses.SpottingChanged((AiClient)owner, target));

                prevHitTargets = hitTargets.Copy();
            }
            else
                EventManager.Instance.TriggerEvent(new Events.Senses.SpottingChanged((AiClient)owner, target));

            prevTarget = target;
                
            yield return null;
        }
    }

    private GameObject FindClosest(Vector3 ownerPos)
    {
        FindVisibleTargets(ownerPos);

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

    private void FindVisibleTargets(Vector3 ownerPos)
    {
        hitTargets = Physics.OverlapSphere(ownerPos, range, hitLayer).ToList();

        foreach (Collider hit in hitTargets)
        {
            if (!(Vector3.Angle(ownerPos, hit.transform.position) <= angle / 2))
                hitTargets.Remove(hit);
            if (Physics.Raycast(new Ray(ownerPos, hit.transform.position), range, obstacleLayer))
                hitTargets.Remove(hit);
        }
    }
}
