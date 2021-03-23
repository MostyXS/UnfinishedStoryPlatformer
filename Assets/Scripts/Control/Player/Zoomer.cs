using Cinemachine;
using Game.PreferencesControl;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Control
{
    public class Zoomer : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera pCam;
        [SerializeField] Transform followPoint;
        [SerializeField] float zoomRadius = 10f;
        [SerializeField] float speed = 5;

        private Transform _mainCameraTransform;
        private bool _isZoomed;
        private Vector2 _axis;

        private void Start()
        {
            if (Camera.main != null) _mainCameraTransform = Camera.main.transform;
        }

        [UsedImplicitly]
        public void OnToggleZoom(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                _isZoomed = true;
                Cursor.visible = false;

                Cursor.lockState = CursorLockMode.Locked;
                pCam.Follow = followPoint;
            }
            else if (ctx.canceled)
            {
                _isZoomed = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pCam.Follow = transform;
                followPoint.position = transform.position;
            }
        }

        [UsedImplicitly]
        public void OnControlZoom(InputAction.CallbackContext ctx)
        {
            _axis = ctx.ReadValue<Vector2>().normalized;
        }

        private void LateUpdate()
        {
            if (_isZoomed)
            {
                ControlZoom();
            }
        }


        private void ControlZoom()
        {
            Debug.Log(PrefsController.ZoomSensitivity);
            var movePosition = _axis * (Time.deltaTime * speed * PrefsController.ZoomSensitivity);
            var newLocation = followPoint.position + (Vector3) movePosition;
            float distanceBetweenCenterAndPoint = Vector2.Distance(followPoint.position, transform.position);
            if (distanceBetweenCenterAndPoint > zoomRadius)
            {
                Vector2 fromOriginToObject = newLocation - transform.position;
                fromOriginToObject *= zoomRadius / distanceBetweenCenterAndPoint;
                newLocation = (Vector2) transform.position + fromOriginToObject;
            }

            
            followPoint.position = newLocation;
            if (Vector2.Distance(_mainCameraTransform.position, followPoint.position) > .1f)
            {
                followPoint.position = new Vector2(_mainCameraTransform.position.x, _mainCameraTransform.position.y);
            }
        }
    }
}