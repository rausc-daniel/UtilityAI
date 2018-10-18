using UnityEditor;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    [SerializeField] private float spread;
    [SerializeField] private float range;
    [SerializeField] private int density;
    private Ray[] rays;
    private float step;

    private void Start()
    {
        step = spread / density;
        rays = new Ray[density];
        Debug.Log(step);
    }

    private void Update()
    {
        int index = 0;

        for (float f = -spread / 2; f < spread / 2; f += step)
        {
            rays[index] = new Ray(transform.position,
                RotatePointAroundPivot(transform.forward * range, transform.position, transform.up * f));
            Debug.Log(rays[index]);
            index++;
        }
    }

    public void FixedUpdate()
    {
        foreach (var ray in rays)
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
        }
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 anchor, Vector3 angles)
    {
        Vector3 dir = point - anchor; // get point direction relative to anchor
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + anchor; // calculate rotated point

        return point; // return it
    }
}