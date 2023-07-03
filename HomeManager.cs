using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene("Main");
        
    }
    public void Home()
    {
        SceneManager.LoadScene("Home");

    }
    public void Wining()
    {
        SceneManager.LoadScene("WIN");
    }
    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

}
