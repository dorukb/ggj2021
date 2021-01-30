using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public GameObject journalUI;

    public List<GameObject> uiEntries;
    public static List<Item> itemsFound = new List<Item>();

    bool open = false;

    public void OpenJournal()
    {
        if (open)
        {
            CloseJournal();
        }
        else
        {
            FindObjectOfType<GameManager>().PauseWithoutUI();
            journalUI.SetActive(true);
            open = true;
        }
    }
    public void CloseJournal()
    {
        journalUI.SetActive(false);
        FindObjectOfType<GameManager>().ResumeButtonCallback();
        open = false;

    }
    private void Start()
    {
        UpdateUI();
    }
    public void AddItem(Item item)
    {
        itemsFound.Add(item);

        UpdateUI();
    }
    void UpdateUI()
    {
        int i = 0;
        for (i = 0; i < itemsFound.Count; i++)
        {
            uiEntries[i].gameObject.SetActive(true);
            JournalEntry je = uiEntries[i].GetComponent<JournalEntry>();
            je.Setup(itemsFound[i].visual, itemsFound[i].desc);
        }
        for (; i < uiEntries.Count; i++)
        {
            uiEntries[i].gameObject.SetActive(false);
        }
    }
    public void RemoveItem(int index)
    {
        if(index < itemsFound.Count) itemsFound.RemoveAt(index);
    }
}
