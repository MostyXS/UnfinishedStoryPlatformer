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
        [SerializeField] private float maxDistanceFromFollowPointToCamera = .3f;

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
            if(!ctx.performed) return;
            ToggleZoom(false);
            GetComponent<Player>().ResetActionMap();
        }

        public void ToggleZoom(bool enable)
        {
            _isZoomed = enable;
            Cursor.visible = !enable;
            if (enable)
            {
                Cursor.lockState = CursorLockMode.Locked;
                pCam.Follow = followPoint;
            }
            else
            {
                
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
            if (!_isZoomed) return;

            ControlZoom();

        }


        private void ControlZoom()
        {
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
            if (Vector2.Distance(_mainCameraTransform.position, followPoint.position) > maxDistanceFromFollowPointToCamera)
            {
                followPoint.position = new Vector2(_mainCameraTransform.position.x, _mainCameraTransform.position.y);
            }
        }
    }
}