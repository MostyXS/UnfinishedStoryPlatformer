using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera pCam;
    [SerializeField] Transform mousePoint;
    [SerializeField] float zoomRadius = 10f;
    
    Camera mCamera;

    private void Start()
    {
        mCamera = Camera.main;
    }
    
    private void LateUpdate()
    {
        if (Input.GetButton("Zoom"))
        {
            pCam.Follow = mousePoint;

            var newLocation = mCamera.ScreenToWorldPoint(Input.mousePosition);
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
