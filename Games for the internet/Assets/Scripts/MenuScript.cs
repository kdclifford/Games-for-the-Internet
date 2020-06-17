using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject customMenu;
    public GameObject controllerInfo;

    public void setMainMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

   public  void setCustomMenu()
    {
        customMenu.SetActive(!customMenu.activeSelf);
        setMainMenu();
    }

    public void SwitcLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SwitcLevelTut()
    {
        SceneManager.LoadScene("Tutorial Level");
    }

    public void ControllerOpenInfo()
    {
        controllerInfo.SetActive(true);
        setMainMenu();
    }

    public void ControllerCloseInfo()
    {
        controllerInfo.SetActive(false);
        setMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
