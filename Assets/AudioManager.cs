using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //public static AudioManager instance;
    public static float volumeLevel = 0.5f;
    public AudioSource bgSource;
    public AudioSource fxSource;
    public static bool hasOne = false;
    public AudioClip bgMusic;
    public AudioClip moveSFX; 
    public AudioClip alarmSFX;

    private void Awake()
    {
        if (hasOne)
        {
            Destroy(this.gameObject);
        }
        else
        {
            hasOne = true;
            DontDestroyOnLoad(this);
            bgSource.clip = bgMusic;
            bgSource.loop = true;
            bgSource.volume = volumeLevel;
            bgSource.Play();
        }
    }

    public void TurnBgMusicOn()
    {
        bgSource.Play();
    }
    public void TurnOffBg()
    {
        bgSource.Stop();
    }


}
