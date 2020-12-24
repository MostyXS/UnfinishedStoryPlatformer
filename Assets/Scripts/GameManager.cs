using Game.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISaveable
{
    public static GameManager Instance { get; private set; }

    List<Decision> decisions = new List<Decision>();


    private void Awake()
    {
        Instance = this;
    }
    public void AddDecision(Decision d)
    {
        decisions.Add(d);
    }

    public List<Decision> GetAllDecisions()
    {
        return decisions;
    }

    public object CaptureState()
    {
        throw new System.NotImplementedException();
    }

    public void RestoreState(object state)
    {
        throw new System.NotImplementedException();
    }
}
