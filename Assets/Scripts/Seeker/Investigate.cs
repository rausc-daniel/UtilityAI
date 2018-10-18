using System.Collections;
using EventSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPatrol", menuName = "Actions/Investigate")]
public class Investigate : Action
{
    [SerializeField] private float radius;
    [SerializeField] private float timeout;

    private Seeker mySeeker;
    private Square investigatingSquare = default(Square);
    private Vector3 currentTarget = default(Vector3);

    public override void Initialize(AiClient client)
    {
        base.Initialize(client);
        mySeeker = (Seeker) MyClient;
        EventManager.Instance.AddListener<Events.UtilityAi.OnActionChanged>(
            e =>
            {
                if (!MyClient.Equals(Events.UtilityAi.OnActionChanged.Origin)) return;

                if (Events.UtilityAi.OnActionChanged.Action.GetType() != typeof(Investigate))
                {
                    investigatingSquare = default(Square);
                    currentTarget = Vector3.zero;
                }
                else
                    investigatingSquare = new Square
                    {
                        HigherX = mySeeker.transform.position.x + radius,
                        LowerX = mySeeker.transform.position.x - radius,
                        HigherZ = mySeeker.transform.position.z + radius,
                        LowerZ = mySeeker.transform.position.z - radius
                    };
            });
    }

    public override float Evaluate()
    {
        float score = Scorers[0].Score;

        DecliningScorer scorer = (DecliningScorer) Scorers[0];
        scorer.DeclineScore();

        return score;
    }

    public override void Execute()
    {
        if (Vector3.Distance(mySeeker.transform.position, currentTarget) < 0.5f || currentTarget == default(Vector3))
        {
            mySeeker.Agent.SetDestination(new Vector3(
                Random.Range(investigatingSquare.LowerX, investigatingSquare.HigherX), 0,
                Random.Range(investigatingSquare.LowerZ, investigatingSquare.HigherZ)));
            mySeeker.Agent.SetDestination(currentTarget);
            MyClient.StartCoroutine(Timer());
        }

        base.Execute();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeout);

        currentTarget = default(Vector3);
    }
}
