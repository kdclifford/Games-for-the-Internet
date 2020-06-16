using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    //Load Menu
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPreviousLevel()
    {
        SceneManager.LoadScene(gameManager.GetComponent<PreviousLevel>().previousLevel);
    }
}
