using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundClips;

    private string currentScene;

    public static AudioManager instance;
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        BackGroundMusic();

    }

    private void Update()
    {
        if(currentScene != SceneManager.GetActiveScene().name)
        {
            BackGroundMusic();
            currentScene = SceneManager.GetActiveScene().name;
        }
        
    }

    void BackGroundMusic()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Play("MainMenu", gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "GameOver")
        {
            Play("GameOver", gameObject);
        }
        else
        {
            Play("BackGroundMusic", gameObject);
        }
    }



    public void Play(string name, GameObject agent)
    {
        AudioSource agentAudio;
        if (agent.GetComponent<AudioSource>() == null)
        {
            agentAudio = agent.AddComponent<AudioSource>();
        }
        else
        {
            agentAudio = agent.GetComponent<AudioSource>();
        }

       Sound s = Array.Find(soundClips, Sound => Sound.name == name);       

        agentAudio.clip = s.clip;
        agentAudio.loop = s.loop;
        agentAudio.volume = s.volume;
        agentAudio.pitch = s.pitch;
        if(s.Sound3D)
        {
            agentAudio.spatialBlend = 1;
        }
        else
        {
            agentAudio.spatialBlend = 0;
        }

        agentAudio.Play();
    }
   
}
