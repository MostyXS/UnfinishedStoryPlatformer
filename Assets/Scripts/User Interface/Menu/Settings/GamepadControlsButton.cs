using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Menu.UI
{
    public class GamepadControlsButton : MonoBehaviour
    {
        [SerializeField] private Transform gamepadSettingsUI;
        private Button _button;


        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Update()
        {
            if (gamepadSettingsUI.gameObject.activeSelf && Gamepad.all.Count == 0)
                gamepadSettingsUI.gameObject.SetActive(false);
            _button.interactable = Gamepad.all.Count != 0;
        }
    }
}