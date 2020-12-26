using Assets.Scripts.Extensions;
using Game.Extensions;
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

        public PlayerStep(DialogueStepData data) : base(data)
        {
        }
        public static GameObject ButtonPrefab { private get; set; }

        public override void InsertInInterface(Transform i)
        {
            base.InsertInInterface(i);
            var content = i.Find(UILabels.CONTENT);
            content.Clear();
            CanSkipThroughAnyClick = currentReplicas.Count == 1;
            if (CanSkipThroughAnyClick)
            {
                var text = Object.Instantiate(TextPrefab, content).GetComponent<Text>();
                text.text = currentReplicas[0].GetContent();

            }
            else
            {
                foreach (var r in currentReplicas)
                {
                    var playerButton = Object.Instantiate(ButtonPrefab, content);
                    playerButton.GetComponentInChildren<Text>().text = r.GetContent();
                    playerButton.GetComponent<Button>().onClick.AddListener(delegate { AssignChosenReplica(r, i); });
                }
            }
        }
        public void AssignChosenReplica(DialogueReplica replica, Transform i)
        {
            currentReplicas.Clear();
            currentReplicas.Add(replica);
            ChosenReplica = replica;
            InsertInInterface(i);
        }
        public override void SetCurrentReplica(int[] replicaNumbers)
        {
            if(replicaNumbers.Length == 0)
            {
                currentReplicas.Add(replicas[0]);
                return;
            }
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
