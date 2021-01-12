using Game.Interfaces;
using UnityEngine;

namespace Game.Dialogues.Components
{
    public abstract class DialogueStep : DialogueStepData, IInsertable
    {
        public static GameObject TextPrefab { protected get; set; }
        public static bool CanSkipThroughAnyClick { get; protected set; }
        public DialogueReplica ChosenReplica { get; protected set; }

        public DialogueStep(DialogueStepData data)
        {
            character = data.GetCharacter();
            replicas = data.GetReplicas();
            isPlayer = data.IsPlayerStep();
        }

        public abstract void SetFirstReplica();
        public abstract void SetCurrentReplica(int[] replicaNumbers);

        public virtual void InsertIntoInterface(Transform i)
        {
            character.InsertIntoInterface(i);
        }
        
       

        


    }
}
