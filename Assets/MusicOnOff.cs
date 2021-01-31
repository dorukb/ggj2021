using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnOff : MonoBehaviour
{
    public static bool musicOn = true;
    public void ToggleBGMusic()
    {
        if (musicOn)
        {
            // then off
            FindObjectOfType<AudioManager>().TurnOffBg();
            musicOn = false;
        }
        else
        {
            // turn on
            FindObjectOfType<AudioManager>().TurnBgMusicOn();
            musicOn = true;
        }
    }
}
