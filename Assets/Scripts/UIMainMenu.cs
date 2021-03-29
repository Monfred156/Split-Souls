using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public GameObject audioManager;
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
