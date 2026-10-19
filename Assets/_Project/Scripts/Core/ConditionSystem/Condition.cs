using UnityEngine;
using System;

[Serializable]
public class Condition
{
    // Check if a singular condition is correct

    public string NameOfFlag;
    public string ExpectedValue; // When being used, this should always be a string. Example "true" or "12"

    public bool Evaluate()
    {
        var value = GameState.Instance.GetFlag<object>(NameOfFlag);

        if (value == null) return "false" == ExpectedValue;

        string StringValue = value.ToString().ToLowerInvariant();
        string Expected = ExpectedValue.ToLowerInvariant();

        return StringValue == Expected;
    }
}
