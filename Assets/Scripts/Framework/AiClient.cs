using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public abstract class AiClient : MonoBehaviour
{
    [SerializeField] protected List<Utility> utilities = new List<Utility>();
    private Utility currentAction;
    private Utility pastAction;
    private int actionCount;

    public virtual void InitializeClient()
    {
        actionCount = utilities.Count;
    }

    public virtual void UpdateClient()
    {
        if(actionCount <= 0) return;
        currentAction = EvaluateActions();
        if (pastAction == null || pastAction.GetType() != currentAction.GetType())
        {
            EventManager.Instance.TriggerEvent(new Events.UtilityAi.OnActionChanged(this, currentAction));
            pastAction = currentAction;
        }
        currentAction.Execute();
    }

    private Utility EvaluateActions()
    {
        float[] scores = new float[actionCount];

        for (int index = 0; index < utilities.Count; index++)
        {
            scores[index] = utilities[index].Evaluate();

            if (scores[index] == -1)
            {
                Debug.LogWarning($"The action at index {index} is not defined");
            }
        }
        return utilities[scores.GetHighestIndex()];
    }
}