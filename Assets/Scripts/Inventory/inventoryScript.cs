using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(Item item);

public class InventoryScript : MonoBehaviour
{

    public event ItemCountChanged itemCountChangedEvent;

    public static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }

    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton[] bagButtons;

    //Debugging purposes
    [SerializeField]
    private Item[] items;

    public bool CanAddBag
    {
        get { return MyBags.Count < 4; }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in MyBags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }

            return count;
        }
    }

    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in MyBags)
            {
                count += bag.MyBagScript.MySlots.Count;
            }

            return count;
        }
    }


    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }

    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }
        set
        {
            fromSlot = value;

            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    public Item[] MyItems { get => items; set => items = value; }
    public List<Bag> MyBags
    {
        get
        {
            return bags;
        }
    }

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(MyItems[0]);
        bag.Initialize(8);
        bag.Use();

    }

    private void Start()
    {
    }
    private void Update()
    {
        //Debugging - Adding bagsslotScriptslotScript
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(MyItems[0]);
            bag.Initialize(8);
            AddItem(bag);
        }

        //Debugging - Adding items inside the slots
        if (Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(MyItems[0]);
            bag.Initialize(8);
            AddItem(bag);
        }

        //Debugging - Adding POTIONS into Inventory
        if (Input.GetKeyDown(KeyCode.P))
        {
            //HP Potion
            HealthPotion potion = (HealthPotion)Instantiate(MyItems[1]);
            AddItem(potion);

            //Poison
            Poison poison = (Poison)Instantiate(MyItems[6]);
            AddItem(poison);

        }

        // material
        if (Input.GetKeyDown(KeyCode.M))
        {
            RawDrumstick rawdrumS = (RawDrumstick)Instantiate(MyItems[7]);
            AddItem(rawdrumS);

            Food orange = (Food)Instantiate(MyItems[8]);
            AddItem(orange);

            Food strawberry = (Food)Instantiate(MyItems[9]);
            AddItem(strawberry);
        }

        //Debugging - Adding FOOD into Inventory
        if (Input.GetKeyDown(KeyCode.U))
        {
            //Beer
            Food beer = (Food)Instantiate(MyItems[2]);
            AddItem(beer);

            //Apple
            Food apple = (Food)Instantiate(MyItems[3]);
            AddItem(apple);

            //David
            Food dumbass = (Food)Instantiate(MyItems[4]);
            AddItem(dumbass);

            //Jam
            Food jam = (Food)Instantiate(MyItems[5]);
            AddItem(jam);
        }
    }

    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                MyBags.Add(bag);
                bag.MyBagButton = bagButton;
                bag.MyBagScript.transform.SetSiblingIndex(bagButton.MyBagIndex);
                break;
            }
        }
    }
    public void AddBag(Bag bag, BagButton bagButton)
    {
        MyBags.Add(bag);
        bagButton.MyBag = bag;
        bag.MyBagScript.transform.SetSiblingIndex(bagButton.MyBagIndex);
    }

    public void AddBag(Bag bag, int bagIndex)
    {
        bag.SetupScript();
        MyBags.Add(bag);
        bag.MyBagButton = bagButtons[bagIndex];
        bagButtons[bagIndex].MyBag = bag;
    }

    public void RemoveBag(Bag bag)
    {
        MyBags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }

        return PlaceInEmpty(item);
    }

    private bool PlaceInEmpty(Item item)
    {
        foreach (Bag bag in MyBags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }

        return false;
    }


    public bool PlaceInStack(Item item)
    {
        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }

        return false;
    }
    public void SwapBags(Bag oldBag, Bag newBag)
    {
        int newSlotCount = (MyTotalSlotCount - oldBag.MySlotCount) + newBag.MySlotCount;

        if (newSlotCount - MyFullSlotCount >= 0)
        {
            //Do Swap
            List<Item> bagItems = oldBag.MyBagScript.GetItems();

            RemoveBag(oldBag);

            newBag.MyBagButton = oldBag.MyBagButton;

            newBag.Use();

            foreach (Item item in bagItems)
            {
                if (item != newBag) //No duplicates
                {
                    AddItem(item);
                }
            }

            AddItem(oldBag);

            HandScript.MyInstance.Drop();

            MyInstance.fromSlot = null;

        }
    }

    public void PlaceInSpecific(Item item, int slotIndex, int bagIndex)
    {
        bags[bagIndex].MyBagScript.MySlots[slotIndex].AddItem(item);
    }

    public void OpenClose()
    {

        bool closedBag = MyBags.Find(X => !X.MyBagScript.IsOpen);

        foreach (Bag bag in MyBags)
        {
            if (bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }

    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach (Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }

        return useables;
    }




    public int GetItemCount(string type)
    {
        int itemCount = 0;

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    itemCount += slot.MyItems.Count;
                }
            }
        }

        return itemCount;

    }


    public Stack<Item> GetItems(string type, int count)
    {
        Stack<Item> items = new Stack<Item>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    foreach (Item item in slot.MyItems)
                    {
                        items.Push(item);

                        if (items.Count == count)
                        {
                            return items;
                        }
                    }
                }
            }
        }

        return items;

    }

    public void RemoveItem(Item item)
    {
        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == item.MyTitle)
                {
                    slot.RemoveItem(item);
                    break;
                }
            }
        }
    }

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
}
