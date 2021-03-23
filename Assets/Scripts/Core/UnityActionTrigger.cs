using System;
using Game.Core.Saving;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Core
{
    public class UnityActionTrigger : MonoBehaviour, ISaveable
    {
        [SerializeField] private UnityEvent triggerEnterActions;

        private Collider2D _collider2D;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                triggerEnterActions?.Invoke();
                _collider2D.enabled = false;
            }
        }


        public object CaptureState()
        {
            return GetComponent<Collider2D>().enabled;
        }

        public void RestoreState(object state)
        {
            GetComponent<Collider2D>().enabled = (bool) state;
        }

        public bool ShouldBeSaved()
        {
            return !_collider2D.enabled;
        }
    }
}