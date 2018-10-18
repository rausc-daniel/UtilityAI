using UnityEngine;

[CreateAssetMenu(fileName = "NewHide", menuName = "Actions/Hide")]
class Hide : Action
{
    private Collider[]
    
    public override float Evaluate()
    {
        return Scorers[0].Score;
    }

    public override void Execute()
    {
        
        
        base.Execute();
    }
}
