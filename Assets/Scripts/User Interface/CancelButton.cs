using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace Game.UI.Menu
{
    public class CancelButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void ActivateButton(InputAction.CallbackContext ctx)
        {
            _button.onClick?.Invoke();
        }


        private void OnEnable()
        {
            if (EventSystem.current == null) return;

            StartCoroutine(UseDelay());
        }

        private IEnumerator UseDelay()
        {
            yield return new WaitForSeconds(.2f);
            EventSystem.current.GetComponent<InputSystemUIInputModule>().cancel.ToInputAction().started +=
                ActivateButton;
        }

        private void OnDisable()
        {
            if (EventSystem.current == null) return;

            EventSystem.current.GetComponent<InputSystemUIInputModule>().cancel.ToInputAction().started -=
                ActivateButton;
        }
    }
}