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

    [SerializeField] float jumpPower = 10f;

    private float xAxis;
    int dir;
    float currentVelMultiplier = 0;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        Move();
        Jump();

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
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