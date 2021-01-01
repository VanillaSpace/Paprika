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

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            slotScript slot =  Instantiate(slotPrefab, transform).GetComponent<slotScript>();
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
}
