using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SecurityCamSensor : MonoBehaviour
{
    [Header("Component references")]
    public Light2D light2d;
    public PolygonCollider2D lightCollider;

    [Header("Rotation parameters")]
    public bool shouldRotate = true;
    public float zRot = 40f; // around 40
    public float halfRotationDuration = 2.5f;
    public float waitTimeBetweenRotations = 0.5f;
    public float startDelay = 0f;

    [Header("Detection parameters")]
    public float detectionLimit = 2f;

    // camera turn on/off
    [Header("Turn on/off parameters")]
    public bool shouldTurnOnOff = false;
    public float turnOnOffPeriod = 2f; 
    public float animationDuration = 1f;
    public float onOffStartDelay = 0f;
    float reverseRot = -0.1f;
    float detectionTimer = 0;

    State state = State.idle;
    Color originalColor;
    float originalIntensity;

    bool playerDetected = false;
    bool stateChanged = false;
    bool isActive = false;

    CameraSFX sfx;
    public enum State
    {
        idle = 0,
        detectedIncreasing = 1,
        detectedDecreasing = 2,
        alarm = 3
    }
    private void Start()
    {
        if(shouldRotate) StartCoroutine(Rotate());
        reverseRot = -zRot;
        state = State.idle;
        originalColor = light2d.color;
        originalIntensity = light2d.intensity;

        if (shouldTurnOnOff)
        {
            StartCoroutine(TurnOnOff());
        }

        sfx = FindObjectOfType<CameraSFX>();
    }
    private void OnEnable()
    {
        GameManager.onGameStateChange += handleStateChange;
    }
    private void OnDisable()
    {
        GameManager.onGameStateChange -= handleStateChange;

    }
    private void handleStateChange(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.playing) isActive = true;
        else isActive = false;
        
    }

    private void Update()
    {
        if (!isActive) return;

        switch (state)
        {
            case State.idle:
                if (stateChanged) ShowDefault();
                if (playerDetected)
                {
                    state = State.detectedIncreasing;
                    stateChanged = true;
                }
                break;
            case State.detectedIncreasing:

                // show detected visually, maybe red coloring etc.
                if(stateChanged) ShowThreatIncreasing();

                stateChanged = false;
                detectionTimer += Time.deltaTime;
                if (detectionTimer >= detectionLimit) state = State.alarm;
          
                if(!playerDetected)
                {
                    state = State.detectedDecreasing;
                    stateChanged = true;
                }

                break;
            case State.detectedDecreasing:

                // show threat level decreasing, maybe gray
                if(stateChanged) ShowThreatDecrasing();

                stateChanged = false;
                detectionTimer -= Time.deltaTime;
                if (detectionTimer <= 0) {
                    stateChanged = true;
                    state = State.idle;
                } 
                if (playerDetected) {
                    state = State.detectedIncreasing;
                    stateChanged = true;
                }
                break;
            case State.alarm:
                detectionTimer = 0;
                // give alarm.
                GiveAlarm();

                state = State.idle;
                stateChanged = true;
                break;
            default:
                break;
        }
    }
    void ShowThreatIncreasing() 
    {
        light2d.color = Color.red;
    }

    void ShowThreatDecrasing() 
    {
        light2d.color = Color.gray;

    }
    void ShowDefault()
    {
        light2d.color = originalColor;
    }
    void GiveAlarm() 
    {
        StopAllCoroutines();
        if (sfx) sfx.PlayAlarm();
        FindObjectOfType<GameManager>()?.LevelFailed();
    }
    bool turnedOff = false;
    IEnumerator TurnOnOff()
    {
        yield return new WaitForSeconds(onOffStartDelay);

        while (true)
        {
            float duration = animationDuration;
            float timePassed = 0;
            // TURN OFF SMOOTHLY
            while (timePassed < duration)
            {
                float t = timePassed / duration;
                if (isActive)
                {
                    light2d.intensity = Mathf.Lerp(originalIntensity, 0f, t);
                    timePassed += Time.deltaTime;
                }
                yield return null;
            }
            lightCollider.enabled = false;
            turnedOff = true;
            yield return new WaitForSeconds(turnOnOffPeriod);

            // TURN ON AGAIN, SMOOTHLY OVER 0.5sec
            timePassed = 0;
            while (timePassed < duration)
            {
                float t = timePassed / duration;
                if (isActive)
                {
                    light2d.intensity = Mathf.Lerp(0f, originalIntensity, t);
                    timePassed += Time.deltaTime;
                }
                yield return null;
            }
            lightCollider.enabled = true;
            turnedOff = false;

            yield return new WaitForSeconds(turnOnOffPeriod);

            yield return null;
        }
    }
    IEnumerator Rotate() 
    {
        yield return new WaitForSeconds(startDelay);

        float timePassed = 0f;
        while (true)
        {

            float duration = halfRotationDuration;

            timePassed = 0;
            while (timePassed < duration)
            {
                float t = timePassed / duration;
                float currRot = zRot * t;
                if (isActive && !turnedOff)
                {
                    transform.Rotate(new Vector3(0, 0, currRot * Time.deltaTime));
                    timePassed += Time.deltaTime;
                }
                yield return null;
            }
            timePassed = 0;
            while (timePassed < duration)
            {
                float t = timePassed / duration;
                float currRot = reverseRot * t;
                if (isActive && !turnedOff)
                {
                    transform.Rotate(new Vector3(0, 0, currRot * Time.deltaTime));
                    timePassed += Time.deltaTime;
                }
                yield return null;
            }

            yield return new WaitForSeconds(waitTimeBetweenRotations);
        }
 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            playerDetected = true;
            if (sfx)
            {
                sfx.PlayWarning();
            }
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
