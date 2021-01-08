using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRender;

    [SerializeField]
    private Sprite openSprite, closeSprite;

    private bool isOpen;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private List<Item> items;

    [SerializeField]
    private bagScript bag;

    public void Interact()
    {
        if (isOpen)
        {
            StopInteract();
        }
        else
        {
            AddItems();
            isOpen = true;
            spriteRender.sprite = openSprite;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void StopInteract()
    {
        StoreItems();
        bag.Clear();
        isOpen = false;
        spriteRender.sprite = closeSprite;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public void AddItems()
    {
         if (items != null)
        {
            foreach (Item item in items)
            {
                item.MySlot.AddItem(item);
            }
        }
    }


    public void StoreItems()
    {
        items = bag.GetItems();
    }
}
