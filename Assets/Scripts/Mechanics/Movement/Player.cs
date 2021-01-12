using Assets.Scripts.ConstantData;
using Game.Combat.Common;
using MostyProUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speed settings")]
    [SerializeField] float accelerationSpeed;
    [SerializeField] float deccelerationSpeed;
    [SerializeField] float minSpeed = 0, maxSpeed = 5;

    [Header("Jump Settings")]
    [SerializeField] float jumpPower = 10f;
    [SerializeField] Transform foot1, foot2;
    [SerializeField] float distanceToGround;
    [SerializeField] LayerMask groundMask;

    float defaultXScale;
    float xAxis;
    int dir;
    float currentVelMultiplier = 0;

    Health myHealth;
    Animator anim;
    Rigidbody2D rb;

    #region Unity Methods
    private void Awake()
    {
        myHealth = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultXScale = transform.localScale.x;
    }
    private void Start()
    {
        
        myHealth.onDeath += PerformDeath;
        UIMenu.Instance.onPause += Pause;
        UIMenu.Instance.onResume += () => { enabled = true; };

    }
    private void LateUpdate()
    {
        Attack();
        Move();
        Jump();

    }
    private void OnDrawGizmos()
    {
        if (foot1 != null)
            Gizmos.DrawRay(foot1.position, Vector2.down * distanceToGround);
        if (foot2 != null)
            Gizmos.DrawRay(foot2.position, Vector2.down * distanceToGround);
    }

    #endregion
    #region Control Methods
    private void Attack()
    {
        if (Input.GetButtonDown(AnimNames.ATTACK))
        {
            anim.SetTrigger(AnimNames.ATTACK);
        }
    }
    private void Jump()
    {
        if (CanJump() && Input.GetButtonDown("Jump"))
        {
            rb.velocity += new Vector2(0, jumpPower);
        }
    }
    private void Move()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        if (IsBraking())
        {
            currentVelMultiplier = Mathf.Clamp(currentVelMultiplier - deccelerationSpeed * Time.deltaTime, 0, 1);

            rb.velocity = new Vector2(Mathf.Lerp(minSpeed, maxSpeed, currentVelMultiplier) * dir, rb.velocity.y);
        }
        else
        {
            dir = xAxis > 0 ? 1 : -1;
            currentVelMultiplier = Mathf.Clamp(currentVelMultiplier + accelerationSpeed * Time.deltaTime, 0, 1);

            rb.velocity = new Vector2(Mathf.Lerp(minSpeed, maxSpeed, currentVelMultiplier) * dir, rb.velocity.y);
            transform.localScale = new Vector2(defaultXScale * dir, transform.localScale.y);
        }

    }
    #endregion
    #region Delegated Methods
    public void PerformDeath()
    {
        UIMenu.Instance.ActivateDeathMenu();
    }

    public void Pause()
    {
        enabled = false;
        rb.velocity = new Vector2(0, 0);
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
        return Mathf.Approximately(xAxis, 0) || xAxis > 0 && rb.velocity.x < 0 || xAxis < 0 && rb.velocity.x > 0;
    }
    #endregion
}
