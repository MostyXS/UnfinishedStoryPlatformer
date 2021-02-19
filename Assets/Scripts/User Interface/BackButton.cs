using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BackButton : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
       
    }
    private void ActivateButton(InputAction.CallbackContext ctx)
    {
        button.onClick.Invoke();
    }

    private void OnEnable()
    {
        UIInput.Instance.Cancel.performed += ActivateButton;
    }
    private void OnDisable()
    {
        UIInput.Instance.Cancel.performed -= ActivateButton;

    }
}
