using Game.Dialogues.Components;
using MostyProUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Dialogues.Core
{
    public class DialoguePlayer : MonoBehaviour
    {

        [SerializeField] Dialogue playableDialogue;

        public static UnityAction TriggerNextStep { get; private set; }
        public static Transform dHUD { private get; set; }

        DialogueStep currentStep;
        Queue<DialogueStepData> steps = new Queue<DialogueStepData>();
        private int[] nextReplicas;

        private void Awake()
        {
            FillDialogueSteps();
            TriggerNextStep += PlayDialogue;
        }

        public void FillDialogueSteps()
        {
            foreach (var step in playableDialogue.GetSteps())
            {
                steps.Enqueue(step);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            StartDialogue();

            GetComponent<Collider2D>().enabled = false;
        }
        public void StartDialogue()
        {
            dHUD.gameObject.SetActive(true);
            UIMenu.Instance.Pause(false);
            PlayDialogue();
        }

        private void AssignStepValue(DialogueStepData data)
        {
            if (data.IsPlayerStep())
            {
                currentStep = new PlayerStep(data);
            }
            else
            {
                currentStep = new NPCStep(data);
            }
        }

        public void PlayDialogue()
        {

            if (steps.Count == 0)
            {
                EndDialogue();
                return;
            }
            if (currentStep == null)
            {
                AssignStepValue(steps.Dequeue());
                currentStep.SetFirstReplica();

            }
            else
            {
                nextReplicas = currentStep.ChosenReplica.GetNextStepReplicas();
                AssignStepValue(steps.Dequeue()); // TO LOOK AT
                currentStep.SetCurrentReplica(nextReplicas);
            }
            
            currentStep.InsertInInterface(dHUD);
        }

        public void EndDialogue()
        {
            UIMenu.Instance.Resume();
            TriggerNextStep -= PlayDialogue;
            dHUD.gameObject.SetActive(false);
        }





    }
}