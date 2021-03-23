using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Core.Predication
{
    public interface IPredicateEvaluator 
    {
        bool? Evaluate(PredicateType predicate, List<string> parameters);
    }
}