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


        Transform target;
        private bool canShoot = true;

        private void Start()
        {
            target = FindObjectOfType<Player>().transform;
        }
        private void Update()
        {
            if (InAttackRange())
            {
                Vector3 dir = target.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (canShoot)
                {
                    var bullet = Instantiate(projectilePrefab, gunMouth.position, Quaternion.identity);
                    
                    bullet.GetComponent<Rigidbody2D>().AddForce(dir * bulletForce, ForceMode2D.Impulse);
                    StartCoroutine(ShootDelay());
                }
            }
        }


        private IEnumerator ShootDelay()
        {
            canShoot = false;
            yield return new WaitForSeconds(shootDelay);
            canShoot = true;

        }

        private bool InAttackRange()
        {
            return Vector2.Distance(target.position, transform.position) <= shootRange;
        }
    }
}