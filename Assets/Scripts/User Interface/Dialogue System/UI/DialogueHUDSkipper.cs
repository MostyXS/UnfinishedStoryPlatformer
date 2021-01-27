using Game.Dialogues.Components;
using Game.Dialogues.Core;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogues.UI
{
    public class DialogueHUDSkipper : MonoBehaviour
    {
        private void Update()
        {
            if (DialogueStep.CanSkipThroughAnyClick && Input.GetButtonDown("Skip"))
            {
                DialoguePlayer.OnTriggerNextStep();
            }

        }
       
    }
}