using UnityEngine;

public class TestClient : AiClient
{
    [SerializeField] private GameObject target;
    [SerializeField] private float scalar1;

    [SerializeField] private Value<float> val = 0;

    [SerializeField] private float eval;
    [SerializeField] private float distance;
    
    public override void InitializeClient()
    {
        utilities.Add(new Utility(val, Asdf, new InvertedSigmoidFunction(scalar1)));
        utilities.Add(new Utility(val, Qwer, new Base()));
        base.InitializeClient();
    }

    public override void UpdateClient()
    {
        val = (transform.position - target.transform.position).sqrMagnitude;
        eval = utilities[0].Evaluate();
        distance = val;
        base.UpdateClient();
    }

    private void OnValidate()
    {
        if(!Application.isPlaying || utilities == null || val == null || utilities.Count <= 0) return;
        print("Penis");
        utilities[0] = new Utility(val, Asdf, new InvertedSigmoidFunction(scalar1));
    }

    private void Asdf() => Debug.Log(name + " " + "ASDF");
    private void Qwer() => Debug.Log(name + " " + "Qwer");
}
