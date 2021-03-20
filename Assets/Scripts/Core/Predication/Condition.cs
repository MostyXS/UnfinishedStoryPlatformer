using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Core
{
    [System.Serializable]
    public class Condition
    {

        [SerializeField]
        private List<Disjunction> and = new List<Disjunction>();
        [SerializeField]
        private bool negate = false;
        public Condition(Condition otherCondition)
        {
            foreach(Disjunction d in otherCondition.and)
            {
                and.Add(d);
            }
            negate = otherCondition.negate;
        }
        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return and.All((dis) => dis.Check(evaluators)) == !negate;
        }

        [System.Serializable]
        public class Disjunction
        {
            [SerializeField]
            List<Predicate> or;

            public Disjunction()
            {
                or = new List<Predicate>();
            }
            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                return or.Any((predicate) => predicate.Check(evaluators));
            }

#if UNITY_EDITOR
            public List<Predicate> GetConjunctions()
            {
                return or;
            }
#endif

        }
        [System.Serializable]
        public class Predicate
        {
            [SerializeField]
            PredicateType predicateType;
            [SerializeField]
            List<string> parametrs = new List<string>();
            [SerializeField]
            bool negate = false;


            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (var evaluator in evaluators)
                {
                    bool? result = evaluator.Evaluate(predicateType, parametrs);
                    if (result == null) continue;

                    if (result == negate) return false;
                }
                return true;
            }
#if UNITY_EDITOR

            
            public PredicateType GetPredicate()
            {
                return predicateType;
            }
            public IEnumerable<string> GetParametrs()
            {
                return parametrs;
            }
            public void AddNewParametr()
            {
                parametrs.Add("");
            }
            public bool GetNegation()
            {
                return negate;
            }
            public void SetPredicate(PredicateType newPredicate)
            {
                predicateType = newPredicate;
            }
            public void SetNegation(bool newNegation)
            {
                negate = newNegation;
            }

            public void SetParametr(string param, string newParam)
            {
                parametrs[parametrs.IndexOf(param)] = newParam;
            }

            public void RemoveParametr(string paramToRemove)
            {
               
                parametrs.Remove(paramToRemove);
            }
#endif


        }
#if UNITY_EDITOR
        public void SetNegation(bool newNegation)
        {
            this.negate = newNegation;
        }
        public bool GetNegation()
        {
            return negate;
        }
        public List<Disjunction> GetDisjunctions()
        {
            return and;
        }

#endif
    }
}