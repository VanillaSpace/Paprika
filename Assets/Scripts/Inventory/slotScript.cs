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

    public bagScript MyBag { get; set; } //reference to the slot the bag is sitting on

    public bool IsEmpty
    {
        get
        {
            return MyItems.Count == 0;
        }
    }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }

            return true;
        }
    }

    public Item MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return MyItems.Peek();
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
        get {return MyItems.Count; }
    }

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public ObservableStacks<Item> MyItems { get => items; }

    public void Awake()
    {
        MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
        MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
        MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    ///<param name ="item">the item to add </param>
    public bool AddItem(Item item)
    {
        MyItems.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public bool AddItems(ObservableStacks<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }

                AddItem(newItems.Pop());
            }

            return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
          inventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
        }
    }

    public void Clear()
    {
        if (MyItems.Count > 0)
        {
            inventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            MyItems.Clear();
        }
    }

    //Right Click ==> use the item
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) //if we dont have something to move
        {
            if (inventoryScript.MyInstance.FromSlot == null && !IsEmpty)
            {
                HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                inventoryScript.MyInstance.FromSlot = this;
            }
            else if (inventoryScript.MyInstance.FromSlot == null && IsEmpty && (HandScript.MyInstance.MyMoveable is Bags))
            {
                Bags bag = (Bags)HandScript.MyInstance.MyMoveable;

                if (bag.MyBagsScript != MyBag && inventoryScript.MyInstance.MyEmptySlotCount - bag.Slots > 0)
                {
                    AddItem(bag);
                    bag.MyBagButton.RemoveBag();
                    HandScript.MyInstance.Drop();
                }
              
            }
            else if (inventoryScript.MyInstance.FromSlot != null) // if we have something to move
            {
                if (PutItemBack() || MergeItems(inventoryScript.MyInstance.FromSlot) || SwapItems(inventoryScript.MyInstance.FromSlot) || AddItems(inventoryScript.MyInstance.FromSlot.MyItems))
                {
                    HandScript.MyInstance.Drop();
                    inventoryScript.MyInstance.FromSlot = null;
                }
            }

        }

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
        if(!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize)
        {
            MyItems.Push(item);
            item.MySlot = this;
            return true;
        }

        return false;
    }

    private bool PutItemBack()
    {
        if (inventoryScript.MyInstance.FromSlot == this)
        {
            inventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }

        return false;
    }

    private bool SwapItems(slotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount+MyCount > MyItem.MyStackSize)
        {
            ObservableStacks<Item> tmpFrom = new ObservableStacks<Item>(from.MyItems);
            from.MyItems.Clear();
            from.AddItems(MyItems);

            MyItems.Clear();
            AddItems(tmpFrom);

            return true;
        }
        return false;
    }

    private bool MergeItems(slotScript from)
    {
        if (IsEmpty)
        {
            return false; 
        }
        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull)
        {
            int free = MyItem.MyStackSize - MyCount; //How many free slots do we have in the stack

            for (int i = 0; i < free; i++)
            {
                AddItem(from.MyItems.Pop());
            }
            return true;
        }

        return false;
    }

    private void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowToolTip(transform.position, MyItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }


}
