using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHealth : MonoBehaviour
{
    public List<GameObject> healthIndicators; // 3 items.
    public int maxHealth = 3;

    public static int remainingLives = 3;

    private void Start()
    {
        UpdateVisual();
    }
    public void UpdateVisual()
    {
        for(int i = 0; i <= 2; i++)
        {
            if(i < remainingLives) healthIndicators[i].gameObject.SetActive(true);
            else healthIndicators[i].gameObject.SetActive(false);
        }
    }
    
}
