using UnityEngine;
using UnityEngine.Events;

namespace Game.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;

        [HideInInspector] public UnityAction onHealthChange;
        [HideInInspector] public UnityAction onDeath;

        private float _health;

        #region Unity Methods

        private void Awake()
        {
            _health = maxHealth;
        }

        #endregion

        #region Public Methods

        public float GetHealthPercentage()
        {
            return Mathf.Clamp(_health / maxHealth, 0, 1);
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (onHealthChange != null)
                onHealthChange();
            if (_health <= 0)
            {
                if (onDeath != null)
                    onDeath();
                Destroy(gameObject);
            }
        }

        #endregion
    }
}