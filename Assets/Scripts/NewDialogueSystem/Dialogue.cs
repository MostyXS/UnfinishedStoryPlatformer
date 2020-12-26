using Game.Dialogues.Components;
using UnityEngine;

namespace Game.Dialogues.Core
{

    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Game/Dialogues/New Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] DialogueStepData[] steps;

        public DialogueStepData[] GetSteps()
        {
            return steps;
        } 
    }

}
