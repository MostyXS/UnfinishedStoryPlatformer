using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    public static BasePlayerControls.UserInterfaceActions Instance { get; private set; }
    

    private void Awake()
    {
        Instance = new BasePlayerControls().UserInterface;
    }

    private void OnEnable()
    {
        Instance.Enable();
    }
    private void OnDisable()
    {
        Instance.Disable();
    }
}
