using Game.Interfaces;
using UnityEngine;

namespace Game.Dialogues.Components
{
    public abstract class DialogueStep : DialogueStepData, IInsertable
    {
        public static GameObject TextPrefab { protected get; set; }
        public DialogueReplica ChosenReplica { get; protected set; }

        public static bool CanSkipThroughAnyClick { get; protected set; }

        public DialogueStep(DialogueStepData data)
        {
            character = data.character;
            replicas = data.replicas;
            isPlayer = data.isPlayer;
        }

        public abstract void SetFirstReplica();
        public abstract void SetCurrentReplica(int[] replicaNumbers);

        public virtual void InsertInInterface(Transform i)
        {
            character.InsertInInterface(i);
        }
        
       

        


    }
}
