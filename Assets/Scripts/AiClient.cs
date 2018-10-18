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

    private void Awake()
    {
        AiController.Register(this);
        actionCount = actions.Count;
        InitializeClient();
    }

    public void UpdateClient()
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

    private Action EvaluateActions()
    {
        float[] scores = new float[actionCount];

        for (int index = 0; index < actions.Count; index++)
        {
            scores[index] = actions[index].Evaluate();

            if (scores[index] == -1)
            {
                Debug.LogWarning($"The action at index {index} is not defined");
            }
        }
        return actions[scores.GetHighestIndex()];
    }

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
