using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Bag", menuName = "Items/Bag", order = 1)]
public class Bag : Item, IUseable
{
    [SerializeField]
    private int slots;

    [SerializeField]
    private GameObject bagPrefab;

    public BagScript MyBagScript { get; set; }

    public BagButton MyBagButton { get; set; }
    public int MySlotCount
    {
        get
        {
            return slots;
        }
    }



    public void Initialize(int slots)
    {
        this.slots = slots;
    }


    public void Use()
    {
        if (InventoryScript.MyInstance.CanAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(slots);

            if (MyBagButton == null)
            {
                InventoryScript.MyInstance.AddBag(this);
            }
            else
            {
                InventoryScript.MyInstance.AddBag(this, MyBagButton);
            }


        }

    }

    public void SetupScript()
    {
        MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
        MyBagScript.AddSlots(slots);
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n{0} SLOT BAG", slots);
    }
}
