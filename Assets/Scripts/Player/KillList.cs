using Game.Core;
using Game.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillList : MonoBehaviour, ISaveable, IPredicateEvaluator
{
    Dictionary<string, int> killedEnemies = new Dictionary<string, int>();
    

    public bool? Evaluate(PredicateType predicate, List<string> parametrs)
    {
        switch(predicate)
        {
            case PredicateType.HasKill:
                {
                    return killedEnemies.ContainsKey(parametrs[0]);
                }
            case PredicateType.KilledMoreThan:
                {
                    int killedCount;
                    return killedEnemies.TryGetValue(parametrs[0], out killedCount) && killedCount > int.Parse(parametrs[1]);
                }
            default:
                return null;
        }
    }
    public object CaptureState()
    {
        return killedEnemies;
    }
    public void RestoreState(object state)
    {
        var resState = state as Dictionary<string, int>;
        if (resState == null) return;
        killedEnemies = resState;

    }

    public bool ShouldBeSaved()
    {
        return true;
    }
}
