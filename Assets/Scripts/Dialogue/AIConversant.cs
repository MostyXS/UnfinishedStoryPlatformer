using Game.Dialogues;
using System;
using Game.Control;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Dialogues
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] private Sprite conversantImage;
        [SerializeField] private string conversantName;
        [SerializeField] private Dialogue dialogue;

        [UsedImplicitly]
        public void StartDialogue()
        {
            Player.Instance.GetComponent<PlayerConversant>().StartDialgoue(this, dialogue);
        }

        public void TriggerAllActions(string action)
        {
            foreach (var dTrigger in GetComponents<DialogueTrigger>())
            {
                dTrigger.Trigger(action);
            }
        }

        public string GetName()
        {
            return conversantName;
        }

        public Sprite GetImage()
        {
            return conversantImage;
        }
    }
}