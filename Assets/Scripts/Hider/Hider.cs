using UnityEngine.AI;

public class Hider : AiClient
{
    private bool hasBeenSpotted;
    public NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
}
