using Game.Dialogues.Components;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Dialogues.Core
{
    public class DialoguePlayer : MonoBehaviour
    {

        [SerializeField] Dialogue playableDialogue;

        public static UnityAction TriggerNextStep { get; private set; }
        public static bool CanSkipThroughAnyClick { get; private set; }
        public static Transform dHUD { private get; set; }

        DialogueStep currentStep;
        Queue<DialogueStep> steps = new Queue<DialogueStep>();
        private void Awake()
        {
            FillDialogueSteps();
        }

        private void Start()
        {
            TriggerNextStep += PlayDialogue;
        }

        public void FillDialogueSteps()
        {
            foreach (var step in playableDialogue.GetSteps())
            {
                steps.Enqueue(step);
            }
        }

        public void StartDialogue()
        {

            dHUD.gameObject.SetActive(true);
            
        }
        public void PlayDialogue()
        {
            if(steps.Count == 0)
            {
                EndDialogue();
            }
            currentStep = steps.Dequeue();
            currentStep.GetCharacter().InsertInInterface(dHUD);
            currentStep.SetFirstReplica();
            currentStep.InsertInInterface(dHUD);
            if (currentStep.IsPlayerStep())
            {
                var ps = currentStep as PlayerStep;
                CanSkipThroughAnyClick = ps.IsSoloReplica;
            }

        }

        public void EndDialogue()
        {
            dHUD.gameObject.SetActive(false);
        }





    }
}