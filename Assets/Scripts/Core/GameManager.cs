using Game.Core.Saving;
using UnityEngine;

namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public SaveManager Saver { get; private set; }
        

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                Saver = GetComponent<SaveManager>();
            }
        }
    }
}