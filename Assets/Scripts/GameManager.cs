﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject failScreen;
    public GameObject pauseScreen;
    //public GameObject settingsScreen;

    public static Action levelFailed;
    public static Action<GameState> onGameStateChange;
    public static int currLevel = 1;
    public static int failCount = 0;

    public GameState state = GameState.intro;
    public enum GameState
    {
        intro = 0,
        playing = 1,
        paused = 2,
        failed = 3,
        won = 4
    }
    private void Awake()
    {
        if (failCount == 0)
        {
            //restarted properly, select target
            FindObjectOfType<GuessingGame>().SelectSet();
            Debug.Log("Select new set.");
        }
    }
    private void Start()
    {
        state = GameState.intro;
        // wait for intro to play etc.
        //Debug.Log(failCount);
        
        Invoke("GameStart", 0.25f);
    }
    void GameStart()
    {

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
    public void PauseWithoutUI()
    {
        state = GameState.paused;
        onGameStateChange?.Invoke(state);
    }
    public void RestartButtonCallback()
    {
        // restart the scene. change if multiple scenes are added.
        failCount++;
        if(failCount > 2)
        {
            failCount = 0;
            currLevel = 1;
            // delete all items. restart case
            FindObjectOfType<Journal>().RemoveAll();
        }
        else
        {
            // delete last collected item
            FindObjectOfType<Journal>().RemoveItem(currLevel - 1);
        }
        SceneManager.LoadScene(1); // 0 is intro.

    }
    public GameObject guessNotif;
    public void LevelPassed()
    {
        currLevel++;
        if(currLevel == 4) //final level
        {
            guessNotif.SetActive(true);
        }
    }
    public void QuitButtonCallback()
    {
        Application.Quit();

    }
}
