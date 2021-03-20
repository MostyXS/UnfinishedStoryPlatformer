using System;
using System.Collections;
using System.Collections.Generic;
using Game.Saving;
using UnityEngine;
using UnityEngine.Events;

public class UnityActionTrigger : MonoBehaviour, ISaveable
{
    private bool _isUsed;

    [SerializeField] private UnityEvent triggerEnterActions;

    [SerializeField] private UnityEvent triggerStayActions;

    [SerializeField] private UnityEvent triggerExitActions;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerEnterActions?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerStayActions?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerExitActions?.Invoke();
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
        return !_isUsed;
    }
}