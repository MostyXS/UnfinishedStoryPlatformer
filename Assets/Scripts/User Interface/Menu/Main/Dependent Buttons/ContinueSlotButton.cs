using Game.Core;
using Game.Core.Saving;
using TMPro;
using UnityEngine.UI;

namespace Game.Menu.UI
{
    public class ContinueSlotButton : SlotButton
    {
        private void Start()
        {
            var textField = GetComponentInChildren<TextMeshProUGUI>();
            textField.text = $"Slot {slotNumber}";
            bool isSaved = IsSaveSlotExists();
            GetComponent<Button>().interactable = isSaved;
            if (isSaved)
            {
                GetComponent<Button>().onClick.AddListener(Continue);
            }
        }

        private void Continue()
        {
            SaveManager.SetPreferredSlot(slotNumber);
            StartCoroutine(GameManager.Instance.Saver.Load());
        }
    }
}