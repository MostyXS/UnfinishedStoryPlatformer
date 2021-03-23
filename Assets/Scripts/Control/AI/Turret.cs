using System.Collections;
using UnityEngine;

namespace Game.Control.AI
{
    public class Turret : MonoBehaviour
    {
        [Header("Main values")] [SerializeField]
        Transform gunMouth;

        [SerializeField] GameObject projectilePrefab;
        [SerializeField] LayerMask playerMask;

        [Header("Shooting parametrs")] [SerializeField]
        private float shootRange = 5f;

        [SerializeField] private float shootDelay = 2.5f;
        [SerializeField] private float bulletForce = 3f;
        [SerializeField] private int burstQuantity = 3;

        [Tooltip("Delay between shooting while burst is active")] [SerializeField]
        private float burstDelay = .1f;

        private bool _canShoot = true;

        private void Update()
        {
            ProcessShooting();
        }

        private void ProcessShooting()
        {
            if (IsPlayerInLineOfSight())
            {
                if (_canShoot)
                {
                    StartCoroutine(BurstShoot());
                    StartCoroutine(ShootDelay());
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(gunMouth.position, new Vector2(transform.localScale.x, 0) * shootRange);
        }

        private IEnumerator BurstShoot()
        {
            for (int i = 0; i < burstQuantity; i++)
            {
                var bullet = Instantiate(projectilePrefab, gunMouth.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x, 0) * bulletForce,
                    ForceMode2D.Impulse);
                yield return new WaitForSeconds(burstDelay);
            }
        }

        private IEnumerator ShootDelay()
        {
            _canShoot = false;
            yield return new WaitForSeconds(shootDelay);
            _canShoot = true;
        }

        private bool IsPlayerInLineOfSight()
        {
            return Physics2D.Raycast(gunMouth.position, new Vector2(transform.localScale.x, 0), shootRange, playerMask)
                .collider;
        }
    }
}