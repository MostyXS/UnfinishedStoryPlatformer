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
        public static GameObject ButtonPrefab { private get; set; }

        List<DialogueReplica> currentReplicas = new List<DialogueReplica>();
        public PlayerStep(DialogueStepData data) : base(data)
        {
        }

        #region Public Methods
        public override void InsertIntoInterface(Transform i)
        {
            base.InsertIntoInterface(i);
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

        public override void SetFirstReplica()
        {
            foreach (var r in replicas)
            {
                TryAddReplica(r);
            }
        }
        public override void SetCurrentReplica(int[] replicaNumbers)
        {
            if (replicaNumbers.Length < 2) //case if linear replica
            {
                currentReplicas.Add(replicas[0]);
                return;
            }
            foreach (var number in replicaNumbers)
            {
                var r = replicas[number];
                TryAddReplica(r);
            }
        }
        #endregion
        #region Delegated Methods
        private void AssignChosenReplica(DialogueReplica replica, Transform i)
        {
            currentReplicas.Clear();
            currentReplicas.Add(replica);
            ChosenReplica = replica;
            InsertIntoInterface(i);
        }
        #endregion
        #region Private Methods
        private void TryAddReplica(DialogueReplica r)
        {
            var dep = r.GetDecisionDependency();
            if (dep == null || dep.IsDecided())
                currentReplicas.Add(r);
        }
        #endregion
    }
}
