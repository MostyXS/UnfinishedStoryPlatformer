using Game.Core;
using Game.Saving;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DecisionList : MonoBehaviour, ISaveable, IPredicateEvaluator
{

    List<string> decisions = new List<string>();
    public bool? Evaluate(PredicateEnum predicate, List<string> parametrs)
    {
        if (predicate != PredicateEnum.HasDecision) return null;

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

}
