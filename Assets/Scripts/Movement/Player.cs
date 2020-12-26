using Assets.Scripts.ConstantData;
using Game.Combat.Common;
using MostyProUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float accelerationSpeed;
    [SerializeField] float deccelerationSpeed;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    [Header("Jump Settings")]
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float distanceToGround;
    [SerializeField] LayerMask groundMask;

    private float xAxis;
    int dir;
    float currentVelMultiplier = 0;


    Health myHealth;
    Animator anim;
    Rigidbody2D rb;
    float defaultXScale;

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

    public void PerformDeath()
    {
        UIMenu.Instance.ActivateDeathMenu();
    }

    public void Pause()
    {
        enabled = false;
        rb.velocity = new Vector2(0, 0);
    }
    void LateUpdate()
    {
        Attack();
        Move();
        Jump();

    }

    private void Attack()
    {
        if(Input.GetButtonDown(AnimNames.ATTACK))
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

    private bool CanJump()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distanceToGround,groundMask).collider != null;
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
            transform.localScale =  new Vector2(defaultXScale * dir, transform.localScale.y);
        }
        
    }

    private bool IsBraking()
    {
        return Mathf.Approximately(xAxis, 0) || xAxis > 0 && rb.velocity.x < 0 || xAxis < 0 && rb.velocity.x > 0;
    }
}
/*private void Jump()
{
    if (Input.GetButtonDown("Jump"))
    {
        rb.velocity += new Vector2(0, jumpPower);
    }
}

private void Move()
{
    xAxis = Input.GetAxisRaw("Horizontal");
    dir = xAxis > 0 ? 1 : -1;
    if (!Mathf.Approximately(xAxis, 0))
    {
        rb.velocity = new Vector2(movementSpeed * dir, rb.velocity.y);
    }
    else
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

}*/