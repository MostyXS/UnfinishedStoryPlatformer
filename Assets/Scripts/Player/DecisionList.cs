using Game.Core;
using Game.Saving;
using System.Collections.Generic;
using UnityEngine;

public class DecisionList : MonoBehaviour, ISaveable, IPredicateEvaluator
{

    List<string> decisions = new List<string>();
    public bool? Evaluate(PredicateType predicate, List<string> parametrs)
    {
        if (predicate != PredicateType.HasDecision) return null;

        return decisions.Contains(parametrs[0]);

    }

    public object CaptureState()
    {
        return decisions;
    }
    public void RestoreState(object state)
    {
        List<string> resState = state as List<string>;
        if (resState == null) return;

        decisions = resState;
    }

    public bool ShouldBeSaved()
    {
        return true;
    }
}
