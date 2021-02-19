using Game.Dialogues.Components;
using Game.Utils;
using MostyProUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Dialogues.Core
{
    public class DialoguePlayer : MonoBehaviour
    {

        [SerializeField] Dialogue playableDialogue;
        [SerializeField] float writeDelay = .03f;

        public static UnityAction OnTriggerNextStep { get; private set; }
        public static Transform DHUD { private get; set; }

        DialogueStep currentStep;
        Queue<DialogueStepData> steps = new Queue<DialogueStepData>();
        int[] nextReplicas;

        bool isWriting = false;

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
            //InGameMenuManager.Instance.Pause(false);
            PlayDialogue();
        }
        public void PlayDialogue() // Executes when the dialogue hud is active in update method(Script is on dialogue hud itself
        {
            if (isWriting) return;
            if (steps.Count == 0)
            {
                FinishDialogue();
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
            StartCoroutine(Write()); 
        }
       
        public IEnumerator Write()
        {
            isWriting = true;
            
            var content = DHUD.Find(UILabels.CONTENT);
            while (content.childCount != 1) // case when only text inside content field(Neither buttons or something else)
            {
                yield return null;

            }
            var contentField = content.GetComponentInChildren<Text>();
            var endContent = contentField.text;
            contentField.text = "";
            yield return new WaitForSeconds(.3f);
            foreach (var c in endContent) //c for character
            {
                if (Input.GetButton("Skip"))
                {
                    contentField.text = endContent;
                    break;
                }
                contentField.text += c;
                yield return new WaitForSeconds(writeDelay);
            }
            isWriting = false;
        }

        public void FinishDialogue()
        {
            OnTriggerNextStep -= PlayDialogue;
            //InGameMenuManager.Instance.Resume();
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