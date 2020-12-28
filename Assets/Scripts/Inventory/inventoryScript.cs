using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryScript : MonoBehaviour
{
    int x;

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

    //Debugging purposes
    [SerializeField]
    private Item[] items;

    //Debugging purposes
    private void Awake()
    {
        Bags bag = (Bags)Instantiate(items[0]);
        bag.Initialize(4);
        bag.Use();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging purposes
        
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    Debug.Log("Increase bag size by 4");
        //    Bags bag = (Bags)Instantiate(items[0]);
        //    x += 4;
        //    bag.Initialize(x);
        //    bag.Use();
        //}
    }
}
