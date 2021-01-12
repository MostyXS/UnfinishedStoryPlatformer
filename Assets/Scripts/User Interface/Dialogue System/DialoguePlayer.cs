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

        public static UnityAction OnTriggerNextStep { get; private set; }
        public static Transform DHUD { private get; set; }

        DialogueStep currentStep;
        Queue<DialogueStepData> steps = new Queue<DialogueStepData>();
        int[] nextReplicas;


        #region UnityMethods
        private void Awake()
        {
            FillDialogueSteps();
            OnTriggerNextStep += PlayDialogue;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            StartDialogue();

            GetComponent<Collider2D>().enabled = false;
        }

        #endregion
        public void FillDialogueSteps()
        {
            foreach (var step in playableDialogue.GetSteps())
            {
                steps.Enqueue(step);
            }
        }
        #region Play Methods
        public void StartDialogue()
        {
            DHUD.gameObject.SetActive(true);
            UIMenu.Instance.Pause(false);
            PlayDialogue();
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

            currentStep.InsertIntoInterface(DHUD);
        }
        public void EndDialogue()
        {
            OnTriggerNextStep -= PlayDialogue;
            UIMenu.Instance.Resume();
            DHUD.gameObject.SetActive(false);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Assigns depending on whether the step is player's or not
        /// </summary>
        /// <param name="data"></param>
        private void AssignStepValue(DialogueStepData data)
        {
            if (data.IsPlayerStep()) currentStep = new PlayerStep(data);
            else currentStep = new NPCStep(data);
        }
        #endregion




    }
}