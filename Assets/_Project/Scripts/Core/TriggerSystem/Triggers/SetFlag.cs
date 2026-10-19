using UnityEngine;

[CreateAssetMenu(menuName = "Triggers/Set Flag")]
public class SetFlag : TriggerActionSO
{
    public string NameOfFlag;
    public string Value;

    public override void Execute()
    {
        Debug.Log($"Setting flag: {NameOfFlag} = {Value}");
        GameState.Instance.SetFlag(NameOfFlag, Value);
    }
}   