using System;
using System.Collections;
using MostyProUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    private Button button;
    private bool canUse = true;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void ActivateButton(InputAction.CallbackContext ctx)
    {
        if(!canUse) return;
        button.onClick.Invoke();
    }

    
    private void OnEnable()
    {
        canUse = false;
        EventSystem.current.GetComponent<InputSystemUIInputModule>().cancel.ToInputAction().performed += ActivateButton;
        StartCoroutine(UseDelay());

    }

    private IEnumerator UseDelay()
    {
        yield return new WaitForSeconds(.2f);
        canUse = true;
    }

    private void OnDisable()
    {
        if (EventSystem.current == null) return;

        EventSystem.current.GetComponent<InputSystemUIInputModule>().cancel.ToInputAction().performed -= ActivateButton;
    }
    
}
