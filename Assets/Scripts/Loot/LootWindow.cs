using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{
    public static LootWindow instance;

    public static LootWindow MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LootWindow>();
            }
            return instance;
        }

    }

    [SerializeField]
    private LootButtons[] lootButtons;

    private List<List<Drop>> pages = new List<List<Drop>>();

    private List<Drop> droppedLoot = new List<Drop>();

    private int pageIndex = 0;

    [SerializeField]
    private Text pageNumber;

    [SerializeField]
    private GameObject nextBtn, prevBtn;

    public IInteractable MyInteractable { get; set; }

    //for debuggin only
    [SerializeField]
    private Item[] items;

    public bool IsOpen
    {
        get { return canvasGroup.alpha > 0;}
    }

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void CreatePages(List<Drop> items)
    {
        if (!IsOpen)
        {
            canvasGroup.blocksRaycasts = true;

            List<Drop> page = new List<Drop>();

            droppedLoot = items;

            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == 4 || i == items.Count - 1)
                {
                    pages.Add(page);
                    page = new List<Drop>();
                }
            }

            AddLoot();

            Open();
        }

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
                    lootButtons[i].MyIcon.sprite = pages[pageIndex][i].MyItem.MyIcon;

                    lootButtons[i].MyLoot = pages[pageIndex][i].MyItem;

                    //make sure the loot buttons is visible
                    lootButtons[i].gameObject.SetActive(true);

                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[pages[pageIndex][i].MyItem.MyQuality], pages[pageIndex][i].MyItem.MyTitle);

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

    public void TakeLoot(Item loot)
    {
        Drop drop = pages[pageIndex].Find(x => x.MyItem == loot);

        pages[pageIndex].Remove(drop);

        drop.Remove();
 
        if (pages[pageIndex].Count == 0)
        {
            //Remove the empty page
            pages.Remove(pages[pageIndex]);

            if (pageIndex == pages.Count && pageIndex > 0)
            {
                pageIndex--;
            }

            AddLoot();
        }
    }

    public void Close()
    {
        pages.Clear();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        ClearButtons();

        if (MyInteractable != null)
        {
            MyInteractable.StopInteract();
        }

        MyInteractable = null;
    }



    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
