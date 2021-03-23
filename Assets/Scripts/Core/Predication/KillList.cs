using Game.Core;
using Game.Core.Saving;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Predication
{
    public class KillList : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        private Dictionary<string, int> _killedEnemies = new Dictionary<string, int>();


        public bool? Evaluate(PredicateType predicate, List<string> parameters)
        {
            switch (predicate)
            {
                case PredicateType.HasKill:
                {
                    return _killedEnemies.ContainsKey(parameters[0]);
                }
                case PredicateType.KilledMoreThan:
                {
                    return _killedEnemies.TryGetValue(parameters[0], out var killedCount) &&
                           killedCount > int.Parse(parameters[1]);
                }
                default:
                    return null;
            }
        }

        public object CaptureState()
        {
            return _killedEnemies;
        }

        public void RestoreState(object state)
        {
            var resState = state as Dictionary<string, int>;
            if (resState == null) return;
            _killedEnemies = resState;
        }

        public bool ShouldBeSaved()
        {
            return true;
        }
    }
}