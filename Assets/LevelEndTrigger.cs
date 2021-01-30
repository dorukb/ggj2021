using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    TransitionManager transitionManager;
    private void Start()
    {
        transitionManager = FindObjectOfType<TransitionManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transitionManager.ChangeLevel();
        }
    }
}
