using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Zoomer : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera pCam;
    [SerializeField] Transform mousePoint;
    [SerializeField] float zoomRadius = 10f;
    [SerializeField] float speed = 5;

    
    BasePlayerControls.PlayerActions inputs;

    private void Start()
    {
        inputs = Player.Inputs;
    }
    private void Update()
    {
        Zoom();
    }
    private void Zoom()
    {
        var bControl = inputs.ActivateZoom.activeControl;
        if (bControl != null && bControl.IsPressed())
        {
            var axis = inputs.ControlZoom.ReadValue<Vector2>();
            pCam.Follow = mousePoint;
            var newLocation = mousePoint.position + (Vector3)axis * Time.deltaTime * speed;
            float distanceBetweenCenterAndMouse = Vector2.Distance(mousePoint.position, transform.position);
            if (distanceBetweenCenterAndMouse > zoomRadius)
            {
                Vector2 fromOriginToObject = newLocation - transform.position;
                fromOriginToObject *= zoomRadius / distanceBetweenCenterAndMouse;
                newLocation = (Vector2)transform.position + fromOriginToObject;

            }
            mousePoint.position = newLocation;
        }
        else
        {
            pCam.Follow = transform;
        }

    }

}
