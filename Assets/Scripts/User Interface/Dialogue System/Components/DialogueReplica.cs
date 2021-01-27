using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Dialogues.Components
{
    [System.Serializable]
    public class DialogueReplica
    {
        [TextArea(4, 10)] [SerializeField] string content;
        [SerializeField] Decision decisionDependency;
        [SerializeField] Decision decisionToActivate;
        [Tooltip("From zero, input next replica numbers with comma, or leave empty if only it is linear")] [SerializeField] string nextSteps;

        #region Getters
        public Decision GetDecisionDependency()
        {
            return decisionDependency;
        }

        public string GetContent()
        {
            return content;
        }
        public Decision GetDecisionToActivate()
        {
            return decisionToActivate;
        }
        /// <summary>
        /// returns distinct number array of assigned replicas
        /// </summary>
        /// <returns></returns>
        public int[] GetNextStepReplicas()
        {
            var stringInts = nextSteps.Split(',');
            List<int> nextReplicas = new List<int>();
            foreach(var sI in stringInts)
            {
                int repNumber;
                if(int.TryParse(sI, out repNumber))
                {
                    nextReplicas.Add(repNumber);
                }
            }
            return nextReplicas.Distinct().ToArray();
        }
        #endregion
        public bool HasMorePriorityThan(DialogueReplica comparableReplica)
        {
            var thisDependency = decisionDependency;
            var otherDependency = comparableReplica.decisionDependency;
            return thisDependency.IsDecided() && (!otherDependency.IsDecided() || otherDependency.GetPriority() < thisDependency.GetPriority());
        }

    }

}
