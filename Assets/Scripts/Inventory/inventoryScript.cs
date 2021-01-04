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




    //Debugging purposes
    private void Awake()
    {
      

    }

    private void Update()
    {
        //Debugging - Adding bagsslotScriptslotScript
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bags bag = (Bags)Instantiate(items[0]);
            bag.Initialize(8);
            bag.Use();
        }

        //Debugging - Adding items inside the slots
        if (Input.GetKeyDown(KeyCode.K))
        {
            Bags bag = (Bags)Instantiate(items[0]);
            bag.Initialize(8);
            AddItem(bag);
        }

        //Debugging - Adding Item into Inventory
        if (Input.GetKeyDown(KeyCode.L))
        {
            //HP Potion
            HealthPotion potion = (HealthPotion)Instantiate(items[1]);
            AddItem(potion);

            //Poison
            Poison poison = (Poison)Instantiate(items[2]);
            AddItem(poison);

            //Potato
            David potato = (David)Instantiate(items[3]);
            AddItem(potato);

            //Apple food
            Apple apples = (Apple)Instantiate(items[4]);
            AddItem(apples);
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

    public void AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return;
            }
        }

        PlaceInEmpty(item);

        //CHANGED 
        //foreach (Bags bag in bags)
        //{
        //    if (bag.MyBagsScript.AddItem(item))
        //    {
        //        return;
        //    }
        //}
    }

    private void PlaceInEmpty(Item item)
    {
        foreach (Bags bag in bags)
        {
            if(bag.MyBagsScript.AddItem(item))
            {
                OnItemCountChanged(item);
                return;
            }
        }
    }

    private bool PlaceInStack(Item item)
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

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
}
