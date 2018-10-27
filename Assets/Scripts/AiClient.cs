using System;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class AiClient : MonoBehaviour
{
    [SerializeField] protected List<Action> Actions;
    private Action currentAction;
    private Action pastAction;

    private int actionCount;

    protected virtual void Awake()
    {
        AiController.Register(this);
        actionCount = Actions.Count;
        InitializeClient();
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

    private Action EvaluateActions()
    {
        float[] scores = new float[actionCount];

        for (int index = 0; index < Actions.Count; index++)
        {
            scores[index] = Actions[index].Evaluate();

            if (scores[index] == -1)
            {
                Debug.LogWarning($"The action at index {index} is not defined");
            }
        }
        return Actions[scores.GetHighestIndex()];
    }

    public virtual void InitializeClient()
    {
        throw new NotImplementedException();
    }
}
