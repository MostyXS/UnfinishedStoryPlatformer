using Game.Attributes;
using UnityEngine;


namespace Game.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float damage;

        private void OnCollisionEnter2D(Collision2D other)
        {
            var health = other.collider.GetComponent<Health>();
            if (health)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}