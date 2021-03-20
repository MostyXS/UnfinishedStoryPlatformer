using System.Collections;
using Game.Attributes;
using UnityEngine;

namespace Game.Combat
{
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] float damage = 20f;
        [SerializeField] float attackDelay = 1f;

        private bool _canAttack = true;

        #region Unity Methods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_canAttack) return;
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
            _canAttack = false;
            yield return new WaitForSeconds(attackDelay);
            _canAttack = true;
        }

        #endregion
    }
}