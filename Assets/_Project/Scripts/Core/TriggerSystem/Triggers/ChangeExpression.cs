using UnityEngine;

[CreateAssetMenu(menuName = "Triggers/Change Expression")]
public class ChangeExpression : TriggerActionSO
{
    public Sprite Expression;

    public override void Execute()
    {
        DialogueManager.Instance.SetExpression(Expression);
    }
}