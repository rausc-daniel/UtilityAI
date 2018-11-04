using System;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class AiClient : MonoBehaviour
{
    [SerializeField] private List<Action> actions;
    private readonly List<Scorer> scorers = new List<Scorer>();
    private Action currentAction;
    private Action pastAction;

    private int actionCount;

    protected virtual void Awake()
    {
        AiController.Register(this);
        actionCount = actions.Count;
        InitializeClient();
    }

    public void UpdateClient()
    {
        if(actionCount <= 0) return;

        // choose best Utility
        currentAction = EvaluateActions();

        // notify about a new Utility
        if (pastAction == null || pastAction.GetType() != currentAction.GetType())
        {
            EventManager.Instance.TriggerEvent(new Events.UtilityAi.OnActionChanged(this, currentAction));
            pastAction = currentAction;
        }

        // execute chosen Utility
        currentAction.Execute();
    }

    // Check which action is the most advantageous
    private Action EvaluateActions()
    {
        // accumulating scores
        float[] scores = new float[actionCount];

        for (int index = 0; index < actions.Count; index++)
        {
            scores[index] = actions[index].Evaluate();
        }

        // return the action with the highest score
        return actions[scores.GetHighestIndex()];
    }

    // Constructor
    private void InitializeClient()
    {
        foreach (Action action in actions)
        {
            action.Initialize(this);
            foreach (Scorer scorer in action.Scorers)
            {
                scorer.Initialize(this);
                if(scorers.Contains(scorer)) continue;
                scorers.Add(scorer);
            }
        }
    }
}
