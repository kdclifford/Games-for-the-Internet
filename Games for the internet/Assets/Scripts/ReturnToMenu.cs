using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    //Load Menu
   public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
