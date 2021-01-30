using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    public GameObject notificationUI;
    public GameObject luggageScreen;
    public GameObject luggageUI;

    bool canInteract = false;
    bool alreadyInZoomMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInteract) return;

        if (Input.GetKeyDown(KeyCode.Space) && !alreadyInZoomMode)
        {
            alreadyInZoomMode = true;
            notificationUI.SetActive(false);
            FindObjectOfType<GameManager>().PauseWithoutUI();
            luggageScreen.SetActive(true);
            luggageUI.SetActive(true);
            // pause the rest
            // open luggage scene, show in fullscreen. rest darkened
            // pick/find an item and read it, its a clue!

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyInZoomMode)
        {
            notificationUI.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            notificationUI.SetActive(false);
            canInteract = false;
        }
    }
}
