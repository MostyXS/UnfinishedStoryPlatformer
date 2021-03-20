using System.Collections;
using Game.Core.Saving;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Menu.UI
{
    public class NewGameSlotButton : SlotButton
    {
        private const float TimeToDoubleClick = 1f;
        private bool _isClicked;

        void Start()
        {
            var textField = GetComponentInChildren<TextMeshProUGUI>();
            textField.text = $"Slot {slotNumber}";
            if (IsSaveSlotExists())
            {
                textField.text += "\n(Saved)";
            }

            GetComponent<Button>().onClick.AddListener(SelectSlotAndLoad);
        }

        private void SelectSlotAndLoad()
        {
            if (!IsSaveSlotExists() || _isClicked)
            {
                StartNewGame();
            }
            else
            {
                StartCoroutine(WaitForDoubleClick());
            }
        }

        private void StartNewGame()
        {
            SaveManager.SetPreferredSlot(slotNumber);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private IEnumerator WaitForDoubleClick()
        {
            _isClicked = true;
            var textField = GetComponentInChildren<TextMeshProUGUI>();
            string oldText = textField.text;
            textField.text = "Click to overwrite";
            float time = 0f;
            var newTextColor = textField.color;
            while (time < TimeToDoubleClick)
            {
                newTextColor.a = 1 - time / TimeToDoubleClick;
                textField.color = newTextColor;
                time += Time.deltaTime;
                yield return null;
            }

            newTextColor.a = 1;
            textField.color = newTextColor;
            textField.text = oldText;
            _isClicked = false;
        }
    }
}