using System;
using System.Collections;
using UnityEngine;

namespace Game.Combat.AI
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] Transform gunMouth;
        [SerializeField] float shootRange = 5f;
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] float shootDelay = 2.5f;
        [SerializeField] float bulletForce = 3f;
        [SerializeField] int burstQuantity = 3;
        [Tooltip("Delay between shooting while burst is active")]
        [SerializeField] float burstDelay = .1f;

        private bool canShoot = true;

        private void Update()
        {
            if (IsPlayerInLineOfSight())
            {
                if (canShoot)
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
            for(int i =0; i < burstQuantity; i++)
            {
                var bullet = Instantiate(projectilePrefab, gunMouth.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x,0) * bulletForce, ForceMode2D.Impulse);
                yield return new WaitForSeconds(burstDelay);
            }


        }

        private IEnumerator ShootDelay()
        {
            canShoot = false;
            yield return new WaitForSeconds(shootDelay);
            canShoot = true;

        }

        private bool IsPlayerInLineOfSight()
        {
            var player = Physics2D.Raycast(gunMouth.position, new Vector2(transform.localScale.x, 0), shootRange).collider?.GetComponent<Player>();
            return player != null;
        }
    }
}