using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void CloseThisGame()
    {
        Application.Quit();
    }
    public void RandomlyLoadScene()
    {
        SceneManager.LoadScene(3);
    }
}
