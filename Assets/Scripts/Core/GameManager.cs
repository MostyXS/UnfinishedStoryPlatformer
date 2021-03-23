using Game.Core.Saving;
using System.Collections;
using System.Collections.Generic;
using Game.Collectioning;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public SaveManager Saver { get; private set; }
        public AtlasSaver AtlasSaver { get; private set; }


        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                Saver = GetComponent<SaveManager>();
                AtlasSaver = GetComponent<AtlasSaver>();
            }
        }
    }
}