using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSFX : MonoBehaviour
{

    public float warningVolume = 0.2f;
    public float alarmVolume = 0.1f;
    public AudioClip warningFX;
    public AudioClip alarmFX;
    public AudioSource src;

    public void PlayWarning()
    {
        src.volume = warningVolume;
        src.PlayOneShot(warningFX);
    }
    public void PlayAlarm()
    {
        src.volume = alarmVolume;
        src.PlayOneShot(alarmFX);

    }
}
