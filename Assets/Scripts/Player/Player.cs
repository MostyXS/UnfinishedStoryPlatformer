using System;
using System.Collections;
using Game.Combat.Common;
using Game.Saving;
using Game.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ISaveable
{
    [Header("Speed settings")] [SerializeField]
    private float accelerationSpeed;

    [SerializeField] private float deccelerationSpeed;
    [SerializeField] float minSpeed, maxSpeed = 5;

    [Header("Attack Settings")] [SerializeField]
    private float attackDelay = 1f;

    [Header("Jump Settings")] [SerializeField]
    private float jumpPower = 10f;

    [SerializeField] private Transform foot1, foot2;
    [SerializeField] private float distanceToGround;
    [SerializeField] private LayerMask groundMask;

    private float _defaultXScale;
    private float _xAxis;
    private int _dir;
    private float _currentVelMultiplier;

    private Health _myHealth;
    private Animator _anim;
    private Rigidbody2D _rb;

    private bool _canAttack;

    #region Unity Callbacks

    #region Input Callbacks

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
        if (CanJump())
        {
            _rb.velocity += new Vector2(0, jumpPower);
        }
    }

    #endregion

    #region Private Methods

    private IEnumerator AttackDelay()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
    }

    #endregion

    private void Awake()
    {
        _myHealth = GetComponent<Health>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _defaultXScale = transform.localScale.x;
    }

    private void Start()
    {
        _myHealth.onDeath += PerformDeath;
    }

    private void OnDrawGizmos()
    {
        if (foot1 != null)
            Gizmos.DrawRay(foot1.position, Vector2.down * distanceToGround);
        if (foot2 != null)
            Gizmos.DrawRay(foot2.position, Vector2.down * distanceToGround);
    }

    private void LateUpdate()
    {
        if (IsBraking())
        {
            _currentVelMultiplier = Mathf.Clamp(_currentVelMultiplier - deccelerationSpeed * Time.deltaTime, 0, 1);

            _rb.velocity = new Vector2(Mathf.Lerp(minSpeed, maxSpeed, _currentVelMultiplier) * _dir, _rb.velocity.y);
        }
        else
        {
            _dir = _xAxis > 0 ? 1 : -1;
            _currentVelMultiplier = Mathf.Clamp(_currentVelMultiplier + accelerationSpeed * Time.deltaTime, 0, 1);

            _rb.velocity = new Vector2(Mathf.Lerp(minSpeed, maxSpeed, _currentVelMultiplier) * _dir, _rb.velocity.y);
            transform.localScale = new Vector2(_defaultXScale * _dir, transform.localScale.y);
        }
    }

    #endregion


    #region Delegated Methods

    public void PerformDeath()
    {
    }

    #endregion

    #region Bool Methods

    private bool CanJump()
    {
        return IsOnGround(foot1) || IsOnGround(foot2);
    }

    private bool IsOnGround(Transform foot)
    {
        return Physics2D.Raycast(foot.position, Vector2.down, distanceToGround, groundMask).collider != null;
    }

    private bool IsBraking()
    {
        return Mathf.Approximately(_xAxis, 0) || _xAxis > 0 && _rb.velocity.x < 0 || _xAxis < 0 && _rb.velocity.x > 0;
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