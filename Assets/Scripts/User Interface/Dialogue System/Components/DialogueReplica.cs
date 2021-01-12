using UnityEngine;

namespace Game.Dialogues.Components
{
    [System.Serializable]
    public class DialogueReplica
    {
        [TextArea(4, 10)] [SerializeField] string content;
        [SerializeField] Decision decisionDependency;
        [SerializeField] Decision decisionToActivate;
        [Tooltip("From zero")] [SerializeField] int[] nextSteps;

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

        public int[] GetNextStepReplicas()
        {
            return nextSteps;
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
