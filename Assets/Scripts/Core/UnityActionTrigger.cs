using Game.Core.Saving;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Core
{
    public class UnityActionTrigger : MonoBehaviour, ISaveable
    {
        private bool _isUsed;

        [SerializeField] private UnityEvent triggerEnterActions;


        [SerializeField] private UnityEvent triggerExitActions;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                triggerEnterActions?.Invoke();
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                triggerExitActions?.Invoke();
            }
        }

        [UsedImplicitly]
        public void SetUsed()
        {
            _isUsed = true;
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
            return !_isUsed;
        }
    }
}