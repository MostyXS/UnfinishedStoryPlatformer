using Game.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour, ISaveable
{
    public static GameManager Instance { get; private set; }


    public object CaptureState()
    {
        return null;
    }

    public void RestoreState(object state)
    {
    }
}
