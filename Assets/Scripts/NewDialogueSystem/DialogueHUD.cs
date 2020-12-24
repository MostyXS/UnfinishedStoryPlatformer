using Game.Dialogues.Components;
using Game.Dialogues.Core;
using UnityEngine;

namespace Game.Dialogues.UI
{
    public class DialogueHUD : MonoBehaviour
    {
        [SerializeField] GameObject textPrefab;
        [SerializeField] GameObject playerButtonPrefab;

        private void Awake()
        {
            DialogueStep.TextPrefab = textPrefab;
            PlayerStep.ButtonPrefab = playerButtonPrefab;
            DialoguePlayer.dHUD = transform;
            gameObject.SetActive(false);
        }
        private void Update()
        {
            if (DialoguePlayer.CanSkipThroughAnyClick && Input.GetButtonDown("Skip"))
                DialogueNextStepActivator.TriggerNextStep();
        }

    }

}