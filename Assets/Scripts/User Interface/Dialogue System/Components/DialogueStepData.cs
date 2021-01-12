using Game.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogues.Components
{
    [System.Serializable]
    
    public class DialogueStepData
    {
        //SPLIT player dialogue step and NPC dialogue step
        [SerializeField] protected DialogueCharacter character;
        [SerializeField] protected List<DialogueReplica> replicas;
        [SerializeField] protected bool isPlayer = false;
        public bool IsPlayerStep()
        {
            return isPlayer;
        }
        public DialogueCharacter GetCharacter()
        {
            return character;
        }
        public List<DialogueReplica> GetReplicas()
        {
            return replicas;
        }


    }
}