using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour
{
    public GameObject OpenLuggageImage; 
    public GameObject LuggageSceneUI;

    public GameObject ShowItemUI;
    public Image ItemVisual;

    public List<Item> items;


    List<Item> foundClues;
    bool alreadyPicked = false;
    Item currentItem = null;
    void Start()
    {
        foundClues = new List<Item>();
    }
    public void SetupItems(List<Item> selecteditems)
    {
        items = selecteditems;
    }
    public void PickAnItemCallback()
    {
        //pick and item and show it, then add to the journal.
        if (alreadyPicked) return;

        int cluesFound = Journal.itemsFound.Count;
        if (cluesFound > 3) Debug.Log("shouldnt happen, 4th clue??");

        currentItem = items[cluesFound]; // here pick at random from current "set"
        foundClues.Add(currentItem);
        FindObjectOfType<Journal>().AddItem(currentItem);

        ShowItem(currentItem);
    }
    public void ShowItem(Item currentItem)
    {
        ShowItemUI.SetActive(true);
        OpenLuggageImage.SetActive(false);
        ItemVisual.sprite = currentItem.visual;
    }

    public void EndLuggageScene()
    {
        ShowItemUI.SetActive(false);
        LuggageSceneUI.SetActive(false);
        alreadyPicked = false;

        FindObjectOfType<GameManager>().ResumeButtonCallback();
    }
}
