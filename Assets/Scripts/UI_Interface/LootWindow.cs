using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{
    [SerializeField]
    private LootButtons[] lootButtons;

    private List<List<Item>> pages = new List<List<Item>>();

    private int pageIndex = 0;

    [SerializeField]
    private Text pageNumber;

    [SerializeField]
    private GameObject nextBtn, prevBtn;

     //for debuggin only
    [SerializeField]
    private Item[] items;

    void Start()
    {
        List<Item> tmp = new List<Item>();
        for (int i = 0; i < items.Length; i++)
        {
            tmp.Add(items[i]);
        }

        CreatePages(tmp);
    }

    void Update()
    {

    }

    public void CreatePages(List<Item> items)
    {
        List<Item> page = new List<Item>();

        for (int i = 0; i < items.Count; i++)
        {
            page.Add(items[i]);

            if (page.Count == 4 || i == items.Count -1)
            {
                pages.Add(page);
                page = new List<Item>();
            }
        }

        AddLoot();
    }


    private void AddLoot()
    {
        if (pages.Count > 0)
        {


            pageNumber.text = pageIndex + 1 + "/" + pages.Count;

            prevBtn.SetActive(pageIndex > 0);
            nextBtn.SetActive(pages.Count > 1 && pageIndex < pages.Count - 1);

            // int itemIndex = Random.Range(0,5);

            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    lootButtons[i].MyIcon.sprite = pages[pageIndex][i].MyIcon;

                    lootButtons[i].MyLoot = pages[pageIndex][i];

                    //make sure the loot buttons is visible
                    lootButtons[i].gameObject.SetActive(true);

                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[pages[pageIndex][i].MyQuality], pages[pageIndex][i].MyTitle);

                    //Make sure that the title is correct
                    lootButtons[i].MyTitle.text = title;
                }

            }
        }

    }

    public void ClearButtons()
    {
        foreach (LootButtons btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count -1)
        {
            pageIndex++;
            ClearButtons();
            AddLoot();
        }
    }

    public void PreviousPage()
    {
        if (pageIndex > 0)
        {
            pageIndex--;
            ClearButtons();
            AddLoot();
        }
    }
}
