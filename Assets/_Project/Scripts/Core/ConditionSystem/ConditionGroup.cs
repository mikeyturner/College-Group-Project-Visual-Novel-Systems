using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ConditionGroup
{
    // This will be used for checking groups of conditions before showing things like dialogue and choices

    public List<Condition> Conditions;

    public bool EvaluateAll()
    {
        foreach (var condition in Conditions )
        {
            if (!condition.Evaluate()) return false;
        }

        return true;
    }
}