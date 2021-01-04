using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour, IPointerClickHandler
{
    private Bags bag;

    [SerializeField]
    private Sprite full, empty;

    public Bags MyBag {
        get
        {
            return bag;
        }

        set 
        { 
            if (value != null)
            {
                GetComponent<Image>().sprite = full; 
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }
            bag = value; 
        } 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //holding down left shift and clicking will de-equip the bag
            if (Input.GetKey(KeyCode.LeftShift))
            {
                HandScript.MyInstance.TakeMoveable(MyBag);
            }
            else if (bag != null)
            {
                bag.MyBagsScript.OpenClose();
            }
        }
                
    }

    public void RemoveBag()
    {
        inventoryScript.MyInstance.RemoveBag(MyBag);
        MyBag.MyBagButton = null;

        foreach (Item item in MyBag.MyBagsScript.GetItems())
        {
            inventoryScript.MyInstance.AddItem(item);
        }

        MyBag = null;
    }
}
