using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tools_Interactable : MonoBehaviour
{
    public string name;

    public static tools_Interactable instance;
    public static tools_Interactable MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<tools_Interactable>();
            }
            return instance;
        }

    }


    [SerializeField] private int Inventory_Item_List;
    public int inv_item_list { get => Inventory_Item_List; set => Inventory_Item_List = value; }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        //it will give the player a tool in their bag
        Tools_Scriptable tools = (Tools_Scriptable)Instantiate(InventoryScript.MyInstance.MyItems[inv_item_list]);
        InventoryScript.MyInstance.AddItem(tools);
        
        name = InventoryScript.MyInstance.MyItems[inv_item_list].MyTitle;
        
        Player.MyInstance.GetItem(name);

        Destroy(gameObject);

    }
}
