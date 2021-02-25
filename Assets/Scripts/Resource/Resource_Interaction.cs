using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Interaction : MonoBehaviour
{
    public string name;

    public static Resource_Interaction instance;

    public static Resource_Interaction MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Resource_Interaction>();
            }
            return instance;
        }

    }

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
        Resource_Items resouce = (Resource_Items)Instantiate(inventoryScript.MyInstance.Myitems[10]);
        inventoryScript.MyInstance.AddItem(resouce);
        
        name = inventoryScript.MyInstance.Myitems[10].MyTitle;
        
        Player.MyInstance.GetItem(name);

        Destroy(gameObject);

    }
}
