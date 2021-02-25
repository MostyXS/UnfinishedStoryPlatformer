using Game.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISaveable
{
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }
    public object CaptureState()
    {
        return null;
    }

    public void RestoreState(object state)
    {
    }
}
