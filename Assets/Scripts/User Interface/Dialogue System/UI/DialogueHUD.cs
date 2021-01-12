using Game.Dialogues.Components;
using Game.Dialogues.Core;
using MostyProUI;
using UnityEngine;

namespace Game.Dialogues.UI
{
    public class DialogueHUD : MonoBehaviour
    {
        [SerializeField] GameObject hudPrefab;
        [SerializeField] GameObject textPrefab;
        [SerializeField] GameObject playerButtonPrefab;
        private void Start()
        {
            DialogueStep.TextPrefab = textPrefab;
            PlayerStep.ButtonPrefab = playerButtonPrefab;
            var tempHud = Instantiate(hudPrefab, MainCanvas.Transform);
            DialoguePlayer.DHUD = tempHud.transform;
            tempHud.SetActive(false);
        }
    }
}