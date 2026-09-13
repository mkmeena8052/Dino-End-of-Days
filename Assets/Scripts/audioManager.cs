using System;
using Unity.VisualScripting;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    // FindObjectOfType<AudioManager>().Play();

    [SerializeField] private Sound[] sounds;
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.audioClip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;

        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }
}
