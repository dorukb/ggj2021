using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
public class Movement : MonoBehaviour
{
    public float pushForce = 400f;
    public float maxMoveSpeed = 4f;
    Vector3 translation;

    Vector2 maxMoveVector;
    Rigidbody2D rb;

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
        maxMoveVector = new Vector2(maxMoveSpeed, maxMoveSpeed);
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
    }

}
