using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GamepadControlsButton : MonoBehaviour
{

    [SerializeField] Transform gamepadTransformUI;
    Button thisButton;


    private void Awake()
    {
        thisButton = GetComponent<Button>();
    }

    private void Update()
    {
        if(gamepadTransformUI.gameObject.activeSelf && Gamepad.all.Count == 0)
            gamepadTransformUI.gameObject.SetActive(false);
        thisButton.interactable = Gamepad.all.Count != 0;

    }
}
