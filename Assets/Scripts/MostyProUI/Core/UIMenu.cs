using System;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace MostyProUI
{
    public class UIMenu : MonoBehaviour
    {
        public event Action onResume;
        public event Action onPause;
        public event Action onPlayerDeath;

       
        [SerializeField] GameObject pauseMenuPrefab = null;
        [SerializeField] GameObject deathMenuPrefab = null;


        static GameObject tempPauseMenu;
        static GameObject tempDeathMenu;
        public static bool CanInteractWithGame { get; set; } = true; //Becomes false when player dead and used for prevent sceneLoading after player death 
        public static bool Paused { get; private set; } = false;
        public static UIMenu Instance { get; set; }
        private void Awake()
        {
            Instance = this;
            CanInteractWithGame = true;
        }
        private void Start()
        { 
            
           InstantiateMenus();
        }
        private void InstantiateMenus()
        {
            
            if (MainCanvas.Transform == null)
                Debug.LogError("No main canvas on scene");
            tempPauseMenu = Instantiate(pauseMenuPrefab, MainCanvas.Transform);
            tempDeathMenu = Instantiate(deathMenuPrefab, MainCanvas.Transform);
            tempPauseMenu.SetActive(false);
            tempDeathMenu.SetActive(false);
        }
        void Update()
        {
            if (!Input.GetButtonDown("Cancel")) return;
            if (!CanInteractWithGame) return;
            if (tempPauseMenu.activeInHierarchy)
            {
                DisablePauseMenu();
            }
            else
            {
                tempPauseMenu.SetActive(true);
                Pause(true);
            }
        }

        public void Continue() //Button event, so 
        {
            //Instance required!!
            Instance.DisablePauseMenu();
        }
        public void DisablePauseMenu()
        {
            tempPauseMenu.SetActive(false);
            Resume();
        }
        public void Resume()
        {
            Time.timeScale = 1f;
            Paused = false;
            if (onResume != null)
                onResume();
        }
       
        public void Pause(bool pauseTime)
        {
            Paused = true;
            if(onPause !=null)
                onPause();
            Time.timeScale = Convert.ToInt32(!pauseTime);
        }
        public void Exit()
        {
            SceneManager.LoadScene("Menu");
        }
        public void ActivateDeathMenu()
        {
            if (onPlayerDeath != null)
                onPlayerDeath();
            Pause(true);

            CanInteractWithGame = false;
            tempPauseMenu.SetActive(false);
            tempDeathMenu.SetActive(true);
        }
        
    }
}
