using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class slotScript : MonoBehaviour, IPointerClickHandler, IClickable
{

    public static slotScript instance;

    public static slotScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<slotScript>();
            }
            return instance;
        }

    }

    //FILO
    private Stack<Item> items = new Stack<Item>();

    [SerializeField]
    private Image icon;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    public Item MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return items.Peek();
            }

            return null;
        }
    }

    public Image MyIcon {

        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get {return items.Count; }
    }

    ///<param name ="item">the item to add </param>
    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            items.Pop();
            Player.MyInstance.UpdateStackSize(this);
        }
    }


    //Right Click ==> use the item
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
          UseItem();
        }
    }

    public void UseItem()
    {

        if (MyItem is IUseable)
        {

            (MyItem as IUseable).Use();
        }
        else
        {
            Debug.Log("Not IUseable");
        }

    }
}
