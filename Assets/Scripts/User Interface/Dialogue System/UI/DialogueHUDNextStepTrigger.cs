using Game.Dialogues.Components;
using Game.Dialogues.Core;
using UnityEngine;

namespace Game.Dialogues.UI
{
    public class DialogueHUDNextStepTrigger : MonoBehaviour
    {
        void Update()
        {
            if (DialogueStep.CanSkipThroughAnyClick && Input.GetButtonDown("Skip"))
            {
                DialoguePlayer.OnTriggerNextStep();
            }

        }
    }
}