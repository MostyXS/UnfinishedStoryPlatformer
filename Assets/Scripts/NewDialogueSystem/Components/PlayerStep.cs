using Game.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogues.Components
{
    public class PlayerStep : DialogueStep
    {
        List<DialogueReplica> currentReplicas = new List<DialogueReplica>();
        public bool IsSoloReplica { get; private set; }
        public static GameObject ButtonPrefab { private get; set; }

        public override void InsertInInterface(Transform i)
        {
            
            var content = i.Find(UILabels.CONTENT);
            IsSoloReplica = currentReplicas.Count == 1;
            if (IsSoloReplica)
            {
                var text = Object.Instantiate(TextPrefab, content).GetComponent<Text>();
                text.text = currentReplicas[0].GetContent();

            }
            else
            {
                foreach (var replica in currentReplicas)
                {
                    var playerButton = Object.Instantiate(ButtonPrefab, content);
                    FillButton(replica, playerButton);
                }
            }
        }

        private void FillButton(DialogueReplica replica, GameObject playerButton)
        {
            playerButton.GetComponentInChildren<Text>().text = replica.GetContent();
            playerButton.GetComponent<Button>().onClick.AddListener(() => { ChosenReplica = replica; });
            
        }
        public override void SetCurrentReplica(int[] replicaNumbers)
        {
            int[] distinctIntArray = replicaNumbers.Distinct().ToArray();
            foreach (var i in distinctIntArray)
            {
                var r = replicas[i];
                AddIfDecisionIsNullOrOpened(r);
            }
        }

        public override void SetFirstReplica()
        {
            foreach(var r in replicas)
            {
                AddIfDecisionIsNullOrOpened(r);
            }
        }

        private void AddIfDecisionIsNullOrOpened(DialogueReplica r)
        {
            var dep = r.GetDecisionDependency();
            if (dep == null || dep.IsRelevant())
                currentReplicas.Add(r);
        }
    }
}
