using UnityEngine;

[CreateAssetMenu(menuName = "Triggers/Show Dialogue")]
public class ShowDialogue : TriggerActionSO
{
    public DialogueLineSO FirstLine;

    public override void Execute()
    {
        DialogueManager.Instance.StartDialogue(FirstLine);
    }
}