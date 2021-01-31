using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public static float volumeLevel = 0.5f;
    public AudioSource bgSource;
    public AudioSource fxSource;

    public AudioClip bgMusic;
    public AudioClip moveSFX; 
    public AudioClip alarmSFX;

    private void Awake()
    {
        if (instance == null) instance = this;

        DontDestroyOnLoad(this);
        bgSource.clip = bgMusic;
        bgSource.loop = true;
        bgSource.volume = volumeLevel;
        bgSource.Play();
    }



}
