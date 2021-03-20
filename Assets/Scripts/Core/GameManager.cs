using Game.Saving;
using System.Collections;
using System.Collections.Generic;
using Game.Collectioning;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SaveManager Saver { get; private set; }
    [SerializeField] private Atlas atlas;

    public Atlas Atlas => atlas;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            Saver = GetComponent<SaveManager>();
        }
    }
}