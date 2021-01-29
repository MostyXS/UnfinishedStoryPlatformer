using Game.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, ISaveable
{
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetButtonDown("Pickup"))
        {
            Take();
        }
    }

    protected virtual void Take()
    {
        gameObject.SetActive(false);

    }

    public object CaptureState()
    {
        return gameObject.activeInHierarchy;
    }

    public void RestoreState(object state)
    {
        gameObject.SetActive((bool)state);
    }
}
