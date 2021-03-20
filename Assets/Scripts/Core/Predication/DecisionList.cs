using Game.Core;
using Game.Core.Saving;
using System.Collections.Generic;
using Game.Core.Predication;
using UnityEngine;

namespace Game.Core.Predication
{
    public class DecisionList : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        private List<string> _decisions = new List<string>();

        public bool? Evaluate(PredicateType predicate, List<string> parameters)
        {
            if (predicate != PredicateType.HasDecision) return null;

            return _decisions.Contains(parameters[0]);
        }

        public object CaptureState()
        {
            return _decisions;
        }

        public void RestoreState(object state)
        {
            List<string> resState = state as List<string>;
            if (resState == null) return;

            _decisions = resState;
        }

        public bool ShouldBeSaved()
        {
            return true;
        }
    }
}