using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Line")]
public class DialogueLineSO : ScriptableObject
{
    public string ID;                           // "DLG_001"
    public CharacterSO Character;               // The Character
    [TextArea] public string Text;              // What they'll say

    public List<Condition> Conditions;          // Conditions for the line of dialogue to show
    public List<TriggerActionSO> Triggers;      // Things that trigger as a result of the dialogue
    public DialogueLineSO Next;                 // Next dialogue line
    public List<ChoiceSO> Choices;
}