using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip audioClip;
    public string name;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.3f, 1f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
