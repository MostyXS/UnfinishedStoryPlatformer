using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.Saving;
using UnityEngine;
using UnityEngine.Events;

public class PersistentTrigger : MonoBehaviour, ISaveable
{
    [SerializeField] private UnityEvent triggerEnterActions;
    [SerializeField] private UnityEvent triggerExitActions;
    [SerializeField] private bool disableOnFirstExit;

    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        triggerEnterActions?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        triggerExitActions?.Invoke();

        if (disableOnFirstExit)
        {
            _collider2D.enabled = false;
        }
    }

    public object CaptureState()
    {
        return _collider2D.enabled;
    }

    public void RestoreState(object state)
    {
        _collider2D.enabled = (bool) state;
    }

    public bool ShouldBeSaved()
    {
        return true;
    }
}