using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreviousLevel : MonoBehaviour
{
    public string previousLevel;
    public string currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().name;
        previousLevel = SceneManager.GetActiveScene().name;
    }

    public static PreviousLevel instance;
    void Awake()
    {        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
       if (currentLevel != SceneManager.GetActiveScene().name)
        {
            previousLevel = currentLevel;
            currentLevel = SceneManager.GetActiveScene().name;
        }
        

    }
}
