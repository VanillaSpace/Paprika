using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootWindow : MonoBehaviour
{
    [SerializeField]
    private LootButtons[] lootButtons;

    //for debuggin only
    [SerializeField]
    private Item[] items;

    void Start()
    {
        AddLoot();
    }

   
    void Update()
    {
        
    }

    private void AddLoot()
    {
        int itemIndex = Random.Range(0,5);

        //set loot button icon
        lootButtons[itemIndex].MyIcon.sprite = items[itemIndex].MyIcon;

        lootButtons[itemIndex].MyLoot = items[itemIndex];

        //make sure the loot buttons is visible
        lootButtons[itemIndex].gameObject.SetActive(true);

        string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[items[itemIndex].MyQuality], items[itemIndex].MyTitle);

        //Make sure that the title is correct
        lootButtons[itemIndex].MyTitle.text = title;
    }
}
