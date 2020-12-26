using Assets.Scripts.ConstantData;
using Game.Combat.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float triggerDistance = 10f;
    [SerializeField] float attackDistance = 2f;
    [SerializeField] float attackTime = 2f;

    int dir;
    Animator anim;
    Transform target;
    float defaultScaleX = 1;
    bool isAttacking = false;
    float distanceToTarget;
    Health myHealth;

    private void Awake()
    {
        myHealth = GetComponent<Health>();
        defaultScaleX = Mathf.Abs(transform.localScale.x);
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        myHealth.onDeath += PerformDeath;
        target = FindObjectOfType<Player>().transform;
    }

    public void PerformDeath()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!target) return;
        dir = target.position.x > transform.position.x ? -1 : 1;
        transform.localScale = new Vector2(defaultScaleX * dir, transform.localScale.y);
        distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget <= triggerDistance)
        {
            if (isAttacking) return;
            ProcessMovement();
            ProcessAttack();
        }


    }

    private void ProcessMovement()
    {
        transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime*dir, transform.position.y);
    }

    private void ProcessAttack()
    {
        if( distanceToTarget <= attackDistance)
        {
            anim.SetTrigger(AnimNames.ATTACK);
            StartCoroutine(AttackTime());
        }
    }

    private IEnumerator AttackTime()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }
}
