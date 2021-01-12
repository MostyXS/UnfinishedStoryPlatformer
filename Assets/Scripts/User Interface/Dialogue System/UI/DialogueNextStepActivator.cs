using Game.Dialogues.Core;
using UnityEngine;

namespace Game.Dialogues.UI
{
    public class DialogueNextStepActivator : MonoBehaviour
    {
        public static void TriggerNextStep()
        {
            DialoguePlayer.OnTriggerNextStep();
        }
    }
}