using System;
using UnityEngine;


namespace MostyProUI
{
    public class InGameMenuManager : MonoBehaviour
    {
        public static InGameMenuManager Instance { get; private set; }
        public bool Paused { get; private set; }

        [SerializeField] private GameObject pauseMenuPrefab;
        [SerializeField] private GameObject deathMenuPrefab;

        private GameObject pauseMenu;
        private GameObject deathMenu;


        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            var canvasTransform = MainCanvas.Instance;
            if (MainCanvas.Instance == null) { Debug.LogError("Main Canvas is null..."); return; }
            pauseMenu = Instantiate(pauseMenuPrefab, canvasTransform);
            deathMenu = Instantiate(deathMenuPrefab, canvasTransform);
            UIInput.Instance.Cancel.performed += (ctx) => Pause(true);
        }
        private void Pause(bool pauseTime)
        {
            if (Paused) return;
            Time.timeScale = Convert.ToInt32(!pauseTime);
            Paused = true;
            pauseMenu.SetActive(true);
        }
        
        public void Unpause()
        {
            if (!Paused) return;    
            Time.timeScale = 1f;
            Instance.Paused = false;
            Instance.pauseMenu.SetActive(false);
        }

        public void ActivateDeathMenu()
        {
            deathMenu.SetActive(true);
        }
    }
}
