using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Bag", menuName = "Items/Bag", order = 1)]
public class Bags : Item, IUseable
{
    
    private int slots;

    [SerializeField]
    private GameObject bagPrefab;

    public bagScript MyBagsScript { get; set; }
    public int Slots { get => slots; }


    public void Initialize(int slots)
    {
        this.slots = slots;
    }


    public void Use()
    {

        if (inventoryScript.MyInstance.CanAddBag)
        {
            Remove();
            MyBagsScript = Instantiate(bagPrefab, inventoryScript.MyInstance.transform).GetComponent<bagScript>();
            MyBagsScript.AddSlots(slots);

            inventoryScript.MyInstance.AddBag(this);
        }
    }
}
