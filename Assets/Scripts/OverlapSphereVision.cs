using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class OverlapSphereVision
{
    private readonly MonoBehaviour owner;
    private readonly float range;
    private readonly LayerMask hitLayer;

    private GameObject target;
    private GameObject prevTarget;

    public OverlapSphereVision(MonoBehaviour owner, float range, LayerMask hitLayer)
    {
        this.owner = owner;
        this.range = range;
        this.hitLayer = hitLayer;
    }

    public Collider[] QueryVision()
    {
        return Physics.OverlapSphere(owner.transform.position, range, hitLayer);
    }
}