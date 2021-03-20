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

        #region Unity Methods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!canAttack) return;
            var healthComponent = collision.GetComponent<Health>();
            TryDamageHealth(healthComponent);

        }
        #endregion
        #region Attack Methods
        private void TryDamageHealth(Health health)
        {
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
        #endregion
    }
}