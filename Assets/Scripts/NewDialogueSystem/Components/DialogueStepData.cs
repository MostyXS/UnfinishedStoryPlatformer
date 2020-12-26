using Game.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogues.Components
{
    [System.Serializable]
    
    public class DialogueStepData
    {
        //SPLIT player dialogue step and NPC dialogue step
        [SerializeField] public DialogueCharacter character;
        [SerializeField] public List<DialogueReplica> replicas;
        [SerializeField] public bool isPlayer = false;
        public bool IsPlayerStep()
        {
            return isPlayer;
        }


    }
}