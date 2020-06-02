using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject customMenu;

    public void setMainMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

   public  void setCustomMenu()
    {
        customMenu.SetActive(!customMenu.activeSelf);
        setMainMenu();
    }

    public void SwitcLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
