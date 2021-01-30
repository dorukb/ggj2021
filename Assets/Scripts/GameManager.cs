using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject failScreen;
    public GameObject pauseScreen;
    //public GameObject settingsScreen;

    public static Action levelFailed;
    public static Action<GameState> onGameStateChange;


    public GameState state = GameState.intro;
    public enum GameState
    {
        intro = 0,
        playing = 1,
        paused = 2,
        failed = 3,
        won = 4
    }

    private void Start()
    {
        state = GameState.intro;


        // wait for intro to play etc.

        state = GameState.playing;
        onGameStateChange?.Invoke(state);
    }
    public void LevelFailed()
    {
        state = GameState.failed;
        onGameStateChange?.Invoke(state);

        // might delegate this to UI once its written
        failScreen.SetActive(true);
    }


    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(state == GameState.paused)
            {
                // unpause,
                state = GameState.playing;
                pauseScreen.SetActive(false);
            }
            else
            {
                // enter paused state
                state = GameState.paused;
                pauseScreen.SetActive(true);
            }
            onGameStateChange?.Invoke(state);
        }
    }

    public void ResumeButtonCallback()
    {
        if(state == GameState.paused)
        {
            state = GameState.playing;
            pauseScreen.SetActive(false);
            onGameStateChange?.Invoke(state);
        }
    }

    public void RestartButtonCallback()
    {
        // restart the scene. change if multiple scenes are added.
        SceneManager.LoadScene(0);
    }

}
