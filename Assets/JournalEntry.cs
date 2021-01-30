using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalEntry : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemText;
    public void Setup(Sprite sprite, string desc)
    {
        icon.sprite = sprite;
        itemText.text = desc;
    }
}
