using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public Sprite sprite;
    private Item thisPerson;

    public void SetupPerson(Item personData)
    {
        thisPerson = personData;
        sprite = personData.visual;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GuessingGame>().MaybeGuess(thisPerson);
        }
    }
}
