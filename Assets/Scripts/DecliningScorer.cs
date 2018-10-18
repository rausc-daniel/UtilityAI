using EventSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDecliningScorer", menuName = "Scorer/Declining Scorer")]
public class DecliningScorer : Scorer
{
    [SerializeField] private float startScore;
    [SerializeField] private float step;
    [SerializeField] private string resetAction;

    public override void Initialize(AiClient client)
    {
        score = 0;
        EventManager.Instance.AddListener<Events.UtilityAi.OnActionChanged>(e =>
        {
            if(MyClient.Equals(Events.UtilityAi.OnActionChanged.Origin) && Events.UtilityAi.OnActionChanged.Action.Name == resetAction)
                ResetScore();
        });
        base.Initialize(client);
    }

    public void ResetScore() => score = startScore;

    public void DeclineScore() => score -= step;
}
