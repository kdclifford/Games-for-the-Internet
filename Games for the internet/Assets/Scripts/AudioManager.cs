using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundClips;


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

        agentAudio.Play();
    }
   
}
