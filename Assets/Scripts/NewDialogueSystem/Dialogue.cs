using Game.Dialogues.Components;
using UnityEngine;

namespace Game.Dialogues
{

    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Game/New Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] DialogueStep[] steps;

        public DialogueStep[] GetSteps()
        {
            return steps;
        } 
    }

}
