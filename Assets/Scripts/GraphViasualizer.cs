using System.Collections.Generic;
using UnityEngine;

public class GraphViasualizer : MonoBehaviour
{
    [SerializeField] private float scalar;
    private float prevScalar;

    [SerializeField] private float step;
    private float prevStep;

    [SerializeField] private GameObject prefab;

    private List<Vector3> vecCache = new List<Vector3>();
    private List<GameObject> goCache = new List<GameObject>();
    private ActivationFunction af;
    private GameObject graphParent;
    private GameObject axisParent;

    void Start()
    {
        graphParent = new GameObject()
        {
            name = "GraphParent"
        };
        axisParent = new GameObject()
        {
            name = "AxisParent"
        };
        EvaluateCurve();
        InstantiateCurve();
    }

    private void Update()
    {
        if (scalar != prevScalar || step != prevStep)
        {
            Debug.Log("Penis");
            EvaluateCurve();
            InstantiateCurve();
        }

        prevScalar = scalar;
        prevStep = step;
    }

    private void EvaluateCurve()
    {
        af = new InvertedSigmoidFunction(scalar);
        if (goCache.Count > 0)
            foreach (var go in goCache)
                Destroy(go);
        vecCache.Clear();
        goCache.Clear();

        var a = 0.0f;

        for (int i = 0; i < 100; i++)
        {
            vecCache.Add(new Vector3(a, af.Evaluate(a), 0));
            a += step;
        }
    }

    private void InstantiateCurve()
    {
        foreach (var vec in vecCache)
            goCache.Add(Instantiate(prefab, vec, Quaternion.identity, graphParent.transform));
    }
}