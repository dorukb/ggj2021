using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessingGame : MonoBehaviour
{

    public List<Item> itemSet1; //first three items are clues, last "item" is the person.
    public List<Item> itemSet2;
    public List<Item> itemSet3;

    public List<Item> personsSet1;
    public List<Item> personsSet2;
    public List<Item> personsSet3;

    public static List<Item> selectedSet;
    public static List<Item> selectedPersons;


    public List<Person> persons;

    public Item correctPerson;
    public GameObject confirmationUI; 
    public GameObject guessNotifUI;

    public void SelectSet()
    {
        int rand = Random.Range(1, 4);
        Debug.Log("Selected item set is: " + rand);
        switch (rand)
        {
            case 1:
                selectedSet = itemSet1;
                selectedPersons = personsSet1;
                break;
            case 2:
                selectedSet = itemSet2;
                selectedPersons = personsSet2;

                break;
            case 3:
                selectedSet = itemSet3; 
                selectedPersons = personsSet3;
                break;
            default:
                selectedSet = itemSet1; 
                selectedPersons = personsSet1;
                break;
        }
        correctPerson = selectedPersons[0];
        for(int i = 0; i < persons.Count; i++)
        {
            // currently always the 1st one is correct, can shuffle
            persons[i].SetupPerson(selectedPersons[i]);
        }
        FindObjectOfType<ClueManager>().SetupItems(selectedSet.GetRange(0, itemSet1.Count-1));
    }
    public void Start()
    {
        correctPerson = selectedPersons[0];
        for (int i = 0; i < persons.Count; i++)
        {
            // currently always the 1st one is correct, can shuffle
            persons[i].SetupPerson(selectedPersons[i]);
        }
        FindObjectOfType<ClueManager>().SetupItems(selectedSet.GetRange(0, itemSet1.Count - 1));
    }
    Item recentGuess;
    public void MaybeGuess(Item guess)
    {
        FindObjectOfType<GameManager>().PauseWithoutUI();
        confirmationUI.SetActive(true);
        recentGuess = guess;
    }

    public void YesButton()
    {
        guessNotifUI.SetActive(false);
        FindObjectOfType<GameManager>().ResumeButtonCallback();

        confirmationUI.SetActive(false);
        if (correctPerson == recentGuess)
        {
            //correct ! won
            Debug.Log("You have won!!");

        }
        else
        {
            Debug.Log("Wrong guess!!");
            recentGuess = null;
            FindObjectOfType<GameManager>().LevelFailed();
        }
    }
    public void NoButton()
    {
        FindObjectOfType<GameManager>().ResumeButtonCallback();
        recentGuess = null;
        confirmationUI.SetActive(false);
    }
}
