using Game.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogues.Components
{
    [System.Serializable]
    public abstract class DialogueStep : IInsertable
    {
        //SPLIT player dialogue step and NPC dialogue step
        [SerializeField] protected DialogueCharacter character;
        [SerializeField] protected List<DialogueReplica> replicas;
        [SerializeField] protected bool isPlayer = false;
        public static GameObject TextPrefab { protected get; set; }
        [HideInInspector] public DialogueReplica ChosenReplica { get; protected set; }


        public bool IsPlayerStep()
        {
            return isPlayer;
        }
        public DialogueCharacter GetCharacter()
        {
            return character;
        }
        
        public abstract void SetCurrentReplica(int[] replicaNumbers);

        public abstract void SetFirstReplica();

        

        public abstract void InsertInInterface(Transform i);
        
    }
}