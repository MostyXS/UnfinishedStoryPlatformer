using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("menu_createsave");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("menu");
    }
  
    public void OpenSettings()
    {
        SceneManager.LoadScene("menu_settings");
    }

    public void OpenAboutUs()
    {
        SceneManager.LoadScene("menu_aboutus");
    }

    public void Continue()
    {
        SceneManager.LoadScene("menu_chooseSave");
        //Load this file
        //Open scene through this file parametrs
    }

    public void OpenAtlasThroughSave()
    {
        SceneManager.LoadScene("menu_chooseSave");
        //Load extended atlas menu depending on chosen save file

    }



    public void ExitGame()
    {
        Application.Quit();
    }




    
}
