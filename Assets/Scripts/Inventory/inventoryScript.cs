using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(Item item);

public class inventoryScript : MonoBehaviour
{

    public event ItemCountChanged itemCountChangedEvent;

    public static inventoryScript instance;

    public static inventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<inventoryScript>();
            }
            return instance;
        }

    }

    private slotScript fromSlot;

    private List<Bags> bags = new List<Bags>();

    [SerializeField]
    private BagButton[] bagButtons;

    //Debugging purposes
    [SerializeField]
    private Item[] items;

    public bool CanAddBag
    {
        get { return bags.Count < 4; }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (Bags bag in bags)
            {
                count += bag.MyBagsScript.MyEmptySlotCount;
            }

            return count;
        }
    }



    public slotScript FromSlot
    {
        get
        {
           return fromSlot;
        }
        set 
        {
            fromSlot = value;

            if(value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    public Item[] Myitems { get => items; set => items = value; }
    private void Start()
    {
        Bags bag = (Bags)Instantiate(Myitems[0]);
        bag.Initialize(8);
        bag.Use();
    }
    private void Update()
    {
        //Debugging - Adding bagsslotScriptslotScript
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bags bag = (Bags)Instantiate(Myitems[0]);
            bag.Initialize(8);
            bag.Use();
        }

        //Debugging - Adding items inside the slots
        if (Input.GetKeyDown(KeyCode.K))
        {
            Bags bag = (Bags)Instantiate(Myitems[0]);
            bag.Initialize(8);
            AddItem(bag);
        }

        //Debugging - Adding POTIONS into Inventory
        if (Input.GetKeyDown(KeyCode.P))
        {
            //HP Potion
            HealthPotion potion = (HealthPotion)Instantiate(Myitems[1]);
            AddItem(potion);

            //Poison
            Poison poison = (Poison)Instantiate(Myitems[6]);
            AddItem(poison);

        }

        // material
        if (Input.GetKeyDown(KeyCode.M))
        {
            RawDrumstick rawdrumS = (RawDrumstick)Instantiate(Myitems[7]);
            AddItem(rawdrumS);

            Food orange = (Food)Instantiate(Myitems[8]);
            AddItem(orange);

            Food strawberry = (Food)Instantiate(Myitems[9]);
            AddItem(strawberry);
        }

        //Debugging - Adding FOOD into Inventory
        if (Input.GetKeyDown(KeyCode.U))
        {
            //Beer
            Food beer = (Food)Instantiate(Myitems[2]);
            AddItem(beer);

            //Apple
            Food apple = (Food)Instantiate(Myitems[3]);
            AddItem(apple);

            //David
            Food dumbass = (Food)Instantiate(Myitems[4]);
            AddItem(dumbass);

            //Jam
            Food jam = (Food)Instantiate(Myitems[5]);
            AddItem(jam);
        }
    }
    
    public void AddBag(Bags bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if(bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);
                bag.MyBagButton = bagButton;
                break;
            }
        }
    }

    public void RemoveBag(Bags bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagsScript.gameObject);
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
        foreach (Bags bag in bags)
        {
            if(bag.MyBagsScript.AddItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }

        return false;
    }

    public bool PlaceInStack(Item item)
    {
        foreach (Bags bag in bags)
        {
            foreach (slotScript slots in bag.MyBagsScript.MySlots)
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

    public void OpenClose()
    {
        //if closed bag == true, then open all closed bags
        //if closed bag == false, then close all closed bags

        bool closedBag = bags.Find(X => !X.MyBagsScript.IsOpen);

        foreach (Bags bag in bags)
        {
            if (bag.MyBagsScript.IsOpen != closedBag)
            {
                bag.MyBagsScript.OpenClose();
            }
        }
    }
   
    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bags bag in bags)
        {
            foreach (slotScript slot in bag.MyBagsScript.MySlots)
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

        foreach (Bags bag in bags)
        {
            foreach (slotScript slot in bag.MyBagsScript.MySlots)
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

        foreach (Bags bag in bags)
        {
            foreach (slotScript slot in bag.MyBagsScript.MySlots)
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
      foreach (Bags bag in bags)
        {
            foreach (slotScript slot in bag.MyBagsScript.MySlots)
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
