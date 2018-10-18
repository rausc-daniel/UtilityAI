using System.Reflection;
using EventSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBooleanScorer", menuName = "Scorer/Boolean Scorer")]
public class BooleanScorer : Scorer
{
    [SerializeField] private float onTrue;
    [SerializeField] private float onFalse;
    [SerializeField] private string affectingBoolean;
    private FieldInfo info;

    public override void Initialize(AiClient client)
    {
        score = 0;
        base.Initialize(client);
        info = MyClient.GetType().GetField(affectingBoolean, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        EventManager.Instance.AddListener<Events.UtilityAi.OnValueChanged>(UpdateBool);
    }
        
    private void UpdateBool(GameEvent e)
    {
        score = (bool) info.GetValue(MyClient) ? onTrue : onFalse;
    }
}
