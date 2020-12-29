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

    //Debugging purposes
    private void Awake()
    {
        Bags bag = (Bags)Instantiate(items[0]);
        bag.Initialize(8);
        bag.Use();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bags bag = (Bags)Instantiate(items[0]);
            bag.Initialize(8);
            bag.Use();
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
