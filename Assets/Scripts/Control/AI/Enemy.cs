using System.Collections;
using Game.Attributes;
using Game.Utils;
using UnityEngine;

namespace Game.Control.AI
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 3f;
        [SerializeField] float triggerDistance = 10f;
        [SerializeField] float attackDistance = 2f;
        [SerializeField] float attackTime = 2f;


        private Health _myHealth;
        private Animator _anim;
        private Transform _target;

        private int _direction;
        private float _defaultScaleX = 1;
        private bool _isAttacking;
        private float _distanceToTarget;

        #region Unity Callbacks

        private void Awake()
        {
            _myHealth = GetComponent<Health>();
            _defaultScaleX = Mathf.Abs(transform.localScale.x);
            _anim = GetComponent<Animator>();
        }

        private void Start()
        {
            _myHealth.onDeath += PerformDeath;
            _target = FindObjectOfType<PlayerController>().transform;
        }

        public void PerformDeath()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            if (!_target) return;
            _direction = _target.position.x > transform.position.x ? -1 : 1;
            transform.localScale = new Vector2(_defaultScaleX * _direction, transform.localScale.y);
            _distanceToTarget = Vector2.Distance(transform.position, _target.position);
            if (_distanceToTarget <= triggerDistance)
            {
                if (_isAttacking) return;
                ProcessMovement();
                ProcessAttack();
            }
        }

        #endregion

        private void ProcessMovement()
        {
            transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime * _direction,
                transform.position.y);
        }

        private void ProcessAttack()
        {
            if (_distanceToTarget <= attackDistance)
            {
                _anim.SetTrigger(AnimNames.ATTACK);
                StartCoroutine(AttackTime());
            }
        }

        private IEnumerator AttackTime()
        {
            _isAttacking = true;
            yield return new WaitForSeconds(attackTime);
            _isAttacking = false;
        }
    }
}