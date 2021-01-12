using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Decision", menuName = "Game/Decision")]
public class Decision : ScriptableObject
{
    [SerializeField] string dName;
    [SerializeField] int priority;

    public string GetName()
    {
        return dName;
    }
    public int GetPriority()
    {
        return priority;
    }
    public bool IsDecided()
    {
        return GameManager.Instance.GetAllDecisions().Contains(this);
    }

}
