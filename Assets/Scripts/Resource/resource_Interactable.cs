using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resource_Interactable : MonoBehaviour
{
    public string name;

    public static resource_Interactable instance;
    public static resource_Interactable MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<resource_Interactable>();
            }
            return instance;
        }

    }

    [SerializeField] int Inventory_Item_List;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //it will give the player a log in their bag
        Resource_Items resrouce = (Resource_Items)Instantiate(InventoryScript.MyInstance.MyItems[Inventory_Item_List]);
        InventoryScript.MyInstance.AddItem(resrouce);

        name = InventoryScript.MyInstance.MyItems[Inventory_Item_List].MyTitle;

        Player.MyInstance.GetItem(name);

        Destroy(gameObject);

    }
}
