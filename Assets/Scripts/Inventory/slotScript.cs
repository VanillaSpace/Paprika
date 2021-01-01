using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class slotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler,IPointerExitHandler
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
    private ObservableStacks<Item> items = new ObservableStacks<Item>();

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize;

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

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
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

    public bool StackItem(Item item)
    {
        if(!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize)
        {
            items.Push(item);
            item.MySlot = this;
            return true;
        }

        return false;
    }

    private void UpdateSlot()
    {
        Player.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            Player.MyInstance.ShowToolTip(transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Player.MyInstance.HideToolTip();
    }
}
