using System;
using System.Collections;
using Game.Attributes;
using Game.Core.Saving;
using Game.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Control
{
    public class Player : MonoBehaviour, ISaveable
    {
        public static GameObject Instance { get; private set; }

        [Header("Speed settings")] [SerializeField]
        private float accelerationSpeed;

        [SerializeField] private float decelerationSpeed;

        [SerializeField] float minSpeed, maxSpeed = 5;

        [Header("Attack Settings")] [SerializeField]
        private float attackDelay = 1f;

        [Header("Jump Settings")] [SerializeField]
        private float jumpPower = 10f;

        [SerializeField] private float jumpDelay = .5f;
        [SerializeField] private BoxCollider2D feetCollider;
        [SerializeField] private LayerMask jumpMask;

        private float _defaultXScale;
        private float _xAxis;
        private int _dir;
        private float _currentVelMultiplier;

        private Health _health;
        private Animator _anim;
        private Rigidbody2D _rb;
        private Zoomer _zoomer;
        private PlayerInput _playerInput;

        private bool _canJump = true;
        private bool _canAttack = true;


        #region Unity Callbacks

        private void OnDisable()
        {
            EnableInputs(false);
        }

        private void OnEnable()
        {
            EnableInputs(true);
        }

        private void Awake()
        {
            _health = GetComponent<Health>();
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _zoomer = GetComponent<Zoomer>();
            _playerInput = GetComponent<PlayerInput>();
            _defaultXScale = transform.localScale.x;
            Instance = this.gameObject;
        }

        private void Start()
        {
            _health.onDeath += PerformDeath;
        }


        private void LateUpdate()
        {
            if (IsBraking())
            {
                _currentVelMultiplier = Mathf.Clamp(_currentVelMultiplier - decelerationSpeed * Time.deltaTime, 0, 1);

                _rb.velocity = new Vector2(Mathf.Lerp(minSpeed, maxSpeed, _currentVelMultiplier) * _dir,
                    _rb.velocity.y);
            }
            else
            {
                _dir = _xAxis > 0 ? 1 : -1;
                _currentVelMultiplier = Mathf.Clamp(_currentVelMultiplier + accelerationSpeed * Time.deltaTime, 0, 1);

                _rb.velocity = new Vector2(Mathf.Lerp(minSpeed, maxSpeed, _currentVelMultiplier) * _dir,
                    _rb.velocity.y);
                transform.localScale = new Vector2(_defaultXScale * _dir, transform.localScale.y);
            }
        }

        #endregion

        #region Input Methods

        [UsedImplicitly]
        public void OnMove(InputAction.CallbackContext ctx)
        {
            _xAxis = ctx.ReadValue<float>();
        }

        [UsedImplicitly]
        public void OnAttack(InputAction.CallbackContext ctx)
        {
            if (!_canAttack) return;
            _anim.SetTrigger(AnimNames.ATTACK);
            StartCoroutine(AttackDelay());
        }

        [UsedImplicitly]
        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (_canJump && IsOnGround())
            {
                _rb.velocity += new Vector2(0, jumpPower);
                StartCoroutine(JumpDelay());
            }
        }

        [UsedImplicitly]
        public void OnToggleZoom(InputAction.CallbackContext ctx)
        {
            if(!ctx.performed) return;
            SwitchCurrentActionMap("Zoom");
            _zoomer.ToggleZoom(true);
        }

        private void SwitchCurrentActionMap(string mapName)
        {
            _playerInput.currentActionMap.Disable();
            _playerInput.SwitchCurrentActionMap(mapName);
            _playerInput.currentActionMap.Enable();
        }

        public void ResetActionMap()
        {
            SwitchCurrentActionMap("Player");
        }

        public void EnableInputs(bool isEnable)
        {
            if (isEnable)
            {
                _playerInput.currentActionMap.Enable();
            }
            else
            {
                _playerInput.currentActionMap.Disable();
            }
        }

        #endregion

        #region Private Methods

        private IEnumerator JumpDelay()
        {
            _canJump = false;
            yield return new WaitForSeconds(jumpDelay);
            _canJump = true;
        }

        private IEnumerator AttackDelay()
        {
            _canAttack = false;
            yield return new WaitForSeconds(attackDelay);
            _canAttack = true;
        }

        #endregion


        #region Delegated Methods

        public void PerformDeath()
        {
        }

        #endregion

        #region Bool Methods

        private bool IsOnGround()
        {
            var hit = Physics2D.BoxCast(feetCollider.bounds.center, feetCollider.bounds.size, 0f,
                Vector2.down,
                .1f,
                jumpMask);
            return hit.collider != null;
        }

        private bool IsBraking()
        {
            return Mathf.Approximately(_xAxis, 0) || _xAxis > 0 && _rb.velocity.x < 0 ||
                   _xAxis < 0 && _rb.velocity.x > 0;
        }

        #endregion

        #region Save Methods

        public object CaptureState()
        {
            throw new System.NotImplementedException();
        }

        public void RestoreState(object state)
        {
            throw new System.NotImplementedException();
        }

        public bool ShouldBeSaved()
        {
            return true;
        }

        #endregion
    }
}