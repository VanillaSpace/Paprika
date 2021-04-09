using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tools_Interactable : MonoBehaviour
{
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
    public string name;

    [SerializeField] private int Inventory_Item_List;
    public int inv_item_list { get => Inventory_Item_List; set => Inventory_Item_List = value; }

    public Tools_Scriptable tools;

    private SpriteRenderer sprite;

    private BoxCollider2D boxCollider;

    public const string LAYER_NAME = "Default";

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //it will give the player a tool in their bag
        tools = (Tools_Scriptable)Instantiate(InventoryScript.MyInstance.MyItems[inv_item_list]);

        InventoryScript.MyInstance.AddItem(tools);
        
        name = InventoryScript.MyInstance.MyItems[inv_item_list].MyTitle;
        
        Player.MyInstance.GetItem(name);

        //if we dont hide this and just destroy it, it will become NULL!
        sprite.sortingLayerName = LAYER_NAME;
        boxCollider.enabled = false;

    }
}
