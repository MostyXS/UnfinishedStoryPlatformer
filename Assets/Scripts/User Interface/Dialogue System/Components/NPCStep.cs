using Game.Extensions;
using Game.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogues.Components
{
    public class NPCStep : DialogueStep
    {
        public NPCStep(DialogueStepData data) : base(data)
        {

        }
        #region Public Methods
        public override void InsertIntoInterface(Transform i)
        {
            base.InsertIntoInterface(i);
            var contentTransform = i.Find(UILabels.CONTENT);
            contentTransform.Clear();
            var contentObjectText = Object.Instantiate(TextPrefab, contentTransform).GetComponent<Text>();
            contentObjectText.text = ChosenReplica.GetContent();
            CanSkipThroughAnyClick = true;
        }

        public override void SetFirstReplica()
        {
            ChosenReplica = CalculatePriotirizedReplica(replicas);
        }
        public override void SetCurrentReplica(int[] replicaNumbers)
        {
            if(replicaNumbers.Length == 0)
            {
                ChosenReplica = replicas[0];
                return;
            }
            int[] distinctIntArray = replicaNumbers.Distinct().ToArray(); // defense against same numbers
            List<DialogueReplica> currentReplicas = GetNewReplicas(distinctIntArray);
            ChosenReplica = CalculatePriotirizedReplica(currentReplicas);
        }
        #endregion
        #region Private Methods
        private List<DialogueReplica> GetNewReplicas(int[] distinctIntArray)
        {
            List<DialogueReplica> currentReplicas = new List<DialogueReplica>();
            foreach (var i in distinctIntArray)
            {
                currentReplicas.Add(replicas[i]);
            }

            return currentReplicas;
        }
        private DialogueReplica CalculatePriotirizedReplica(List<DialogueReplica> replicas)
        {
            
            DialogueReplica mvReplica = null;
            foreach (var r in replicas)
            {
                if (mvReplica == null || r.HasMorePriorityThan(mvReplica))
                {
                    mvReplica = r;
                }
            }
            return mvReplica;
        }
        #endregion
    }
}
