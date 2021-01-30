using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransitionManager : MonoBehaviour
{
    public List<Transform> startingPositions;
    public List<Transform> cameraPositions;

    public Transform player;
    public Transform cam;
    public Image fadePanel;
    public float duration = 1f;
    Color transparent = new Color(0, 0, 0, 0);


    public void ChangeLevel()
    {
        GameManager.currLevel++;
        StartFadeOut();
        Invoke("StartFadeIn", duration);

    }
    public void StartFadeIn()
    {
        MovePlayerToStartingPosition();
        MoveCameraToPosition();
        if (fadePanel) StartCoroutine(FadeIn(duration, Color.black, transparent));
    }
    public void MoveCameraToPosition()
    {
        var pos = cam.position;
        pos.x = cameraPositions[GameManager.currLevel - 1].position.x;
        cam.position = pos;
    }
    public void MovePlayerToStartingPosition()
    {
        player.transform.position = startingPositions[GameManager.currLevel - 1].position;
    }

    public void StartFadeOut()
    {
        if (fadePanel) StartCoroutine(FadeOut(duration, transparent, Color.black));
    }
    IEnumerator FadeOut(float duration, Color from, Color to)
    {
        fadePanel.gameObject.SetActive(true);

        float timePassed = 0f;
        float perct;
        while (timePassed < duration)
        {
            perct = timePassed / duration;
            fadePanel.color = Color.Lerp(from, to, perct);

            timePassed += Time.deltaTime;
            yield return null;
        }
        fadePanel.color = to;
    }
    IEnumerator FadeIn(float duration, Color from, Color to)
    {
        fadePanel.gameObject.SetActive(true);

        float timePassed = 0f;
        float perct;
        while (timePassed < duration)
        {
            perct = timePassed / duration;
            fadePanel.color = Color.Lerp(from, to, perct);
            timePassed += Time.deltaTime;
            yield return null;
        }
        fadePanel.color = to;
        fadePanel.gameObject.SetActive(false);
    }
}
