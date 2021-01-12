using MostyProUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat.Common
{
    public class Health : MonoBehaviour
    {

        [SerializeField] float maxHealth = 100f;

        public UnityAction onHealthChange;
        public UnityAction onDeath;
        float health;

        private void Awake()
        {
            health = maxHealth;
        }


        public float GetHealthPercentage()
        {
            return Mathf.Clamp( health / maxHealth, 0,1);
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (onHealthChange != null)
                onHealthChange();
            if (health <= 0)
            {
                if (onDeath != null)
                    onDeath();
                Destroy(gameObject);
            }
        }

    }
}