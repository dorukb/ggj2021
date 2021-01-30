using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
public class Movement : MonoBehaviour
{
    public Transform playerVisuals;
    public float pushForce = 400f;
    public float maxMoveSpeed = 4f;
    public float tiltAmount = 5f;
    public float tiltThreshold = 1.5f;
    Vector3 translation;

    Rigidbody2D rb;
    Vector3 tiltedRight = new Vector3(0, 0, -5f);

    bool processInput = false;
    Vector2 savedVelocity;
    private void OnEnable()
    {
        GameManager.onGameStateChange += handleStateChange;
    }
    private void OnDisable()
    {
        GameManager.onGameStateChange -= handleStateChange;

    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void handleStateChange(GameManager.GameState state)
    {
        if (state != GameState.playing)
        {
            processInput = false;
            savedVelocity = rb.velocity;
            rb.Sleep();
        }
        else { 
            processInput = true;
            rb.WakeUp();
            rb.velocity = savedVelocity;
        }
    }

    void Update() 
    {
        if (!processInput) return;

        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        translation.x = horz;
        translation.y = vert;
    }

    private void FixedUpdate()
    {
        if (!processInput) return;
        //transform.Translate(translation.normalized * moveSpeed * Time.deltaTime);
        rb.AddForce(translation.normalized * pushForce * Time.deltaTime);

        if (rb.velocity.magnitude > maxMoveSpeed) rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxMoveSpeed);

        if (rb.velocity.x > tiltThreshold) playerVisuals.localRotation = Quaternion.Euler(0, 0, -tiltAmount);
        else if (rb.velocity.x < -tiltThreshold) playerVisuals.localRotation = Quaternion.Euler(0, 0, tiltAmount);
        else playerVisuals.localRotation = Quaternion.Euler(0, 0, 0);
    }

}
