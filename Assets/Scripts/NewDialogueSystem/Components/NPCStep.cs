using Game.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogues.Components
{
    public class NPCStep : DialogueStep
    {
        public override void InsertInInterface(Transform i)
        {
            var contentTransform = i.Find(UILabels.CONTENT);
            var contentField = Object.Instantiate(TextPrefab, contentTransform).GetComponent<Text>();
            contentField.text = ChosenReplica.GetContent();
        }

        public override void SetCurrentReplica(int[] replicaNumbers)
        {
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
