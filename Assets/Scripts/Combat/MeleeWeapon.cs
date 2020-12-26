using Game.Combat.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat.Weapons
{
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] float damage = 20f;
        [SerializeField] float attackDelay = 1f;

        bool canAttack = true;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!canAttack) return;
            var health = collision.GetComponent<Health>();
            if (health)
            {
                health.TakeDamage(damage);
                StartCoroutine(AttackDelay());
            }

        }

        private IEnumerator AttackDelay()
        {
            canAttack = false;
            yield return new WaitForSeconds(attackDelay);
            canAttack = true;
        }
    }
}