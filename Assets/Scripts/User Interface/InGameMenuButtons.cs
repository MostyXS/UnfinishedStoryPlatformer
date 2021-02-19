using MostyProUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuButtons : MonoBehaviour
{

    public void Continue()
    {
        InGameMenuManager.Instance.Unpause();
    }
    public void Restart()//???
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
