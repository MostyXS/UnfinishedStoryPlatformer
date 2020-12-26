using Assets.Scripts.Extensions;
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
        public override void InsertInInterface(Transform i)
        {
            base.InsertInInterface(i);
            var content = i.Find(UILabels.CONTENT);
            content.Clear();
            var contentField = Object.Instantiate(TextPrefab, content).GetComponent<Text>();
            contentField.text = ChosenReplica.GetContent();
            CanSkipThroughAnyClick = true;
        }

        public override void SetCurrentReplica(int[] replicaNumbers)
        {
            if(replicaNumbers.Length == 0)
            {
                ChosenReplica = replicas[0];
                return;
            }
            int[] distinctIntArray = replicaNumbers.Distinct().ToArray(); // defense against same numbers
            List<DialogueReplica> currentReplicas = CreateNewReplicasArray(distinctIntArray);
            ChosenReplica = CalculatePriotirizedReplica(currentReplicas);
        }

        private List<DialogueReplica> CreateNewReplicasArray(int[] distinctIntArray)
        {
            List<DialogueReplica> currentReplicas = new List<DialogueReplica>();
            foreach (var i in distinctIntArray)
            {
                currentReplicas.Add(replicas[i]);
            }

            return currentReplicas;
        }

        public override void SetFirstReplica()
        {
            ChosenReplica = CalculatePriotirizedReplica(replicas);
        }
        private DialogueReplica CalculatePriotirizedReplica(List<DialogueReplica> replicas)
        {
            
            DialogueReplica mvReplica = null;
            foreach (var r in replicas)
            {
                if (mvReplica == null || r.IsMoreValuableThan(mvReplica))
                {
                    mvReplica = r;
                }
            }
            return mvReplica;
        }

    }
}
