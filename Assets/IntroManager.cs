using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }
    public void StartPlaying()
    {
        SceneManager.LoadScene(1); // main play scene is 1, 0 is intro
    }

    
}
