using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundClips;


    public void Play(string name, GameObject agent)
    {
       Sound s = Array.Find(soundClips, Sound => Sound.name == name);
        AudioSource agentAudio = agent.GetComponent<AudioSource>();

        agentAudio.clip = s.clip;
        agentAudio.loop = s.loop;
        agentAudio.volume = s.volume;
        agentAudio.pitch = s.pitch;

        agentAudio.Play();
    }
   
}
