using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Choice")]
public class ChoiceSO : ScriptableObject
{
    public string ChoiceText;                   // "Look in the crack"
    public List<Condition> Conditions;          // Conditions to show this choice
    public List<TriggerActionSO> Triggers;      // Fires when this choice is picked
    public DialogueLineSO NextLine;             // Where this choice leads
}