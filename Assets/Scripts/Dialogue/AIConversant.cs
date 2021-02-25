using Game.Dialogues;
using System;
using UnityEngine;

namespace Game.Dialogues
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] private string conversantName;
        [SerializeField] private Dialogue dialogue;
        
        public void TriggerAllActions(string action)
        {
            foreach(var dTrigger in GetComponents<DialogueTrigger>())
            {
                dTrigger.Trigger(action);
            }
        }

        public string GetName()
        {
            return conversantName;
        }
        public Dialogue GetDialogue()
        {
            return dialogue;
        }
    }
}