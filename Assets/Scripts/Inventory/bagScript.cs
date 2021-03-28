using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bagScript : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;

    private CanvasGroup canvasGroup;

    private List<slotScript> slots = new List<slotScript>();

    public bool IsOpen
    {
        get { return canvasGroup.alpha > 0; }
    }

    public List<slotScript> MySlots { get => slots; }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (slotScript slot in MySlots)
            {
                if(slot.IsEmpty)
                {
                    count++;
                }
            }
            return count;
        }   
    }


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public List<Item> GetItems()
    {
        List<Item> items = new List<Item>();

        foreach (slotScript slot in slots)
        {
            if (!slot.IsEmpty)
            {
                foreach (Item item in slot.MyItems)
                {
                    items.Add(item);
                }
            }
        }

        return items;
    }

    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            slotScript slot =  Instantiate(slotPrefab, transform).GetComponent<slotScript>();
            slot.MyIndex = i;
            slot.MyBag = this; 
            MySlots.Add(slot);
        }
    }

    public bool AddItem(Item item)
    {
        foreach (slotScript slot in MySlots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);

                return true;
            }
        }

        return false;
    }

    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void Clear()
    {
        foreach (slotScript slot in slots)
        {
            slot.Clear();
        }
    }
}
