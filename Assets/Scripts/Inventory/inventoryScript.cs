using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryScript : MonoBehaviour
{
  

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
        Bags bag = (Bags)Instantiate(items[0]);
        bag.Initialize(8);
        bag.Use();

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
                break;
            }
        }
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
   
}
