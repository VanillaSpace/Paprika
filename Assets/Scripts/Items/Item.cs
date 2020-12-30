using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    //scripObj = does not need game obj

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

 
    private slotScript slot;


    public Sprite MyIcon 
    {
        get
        { 
            return icon; 
        }
    
    }

    public int StackSize 
    {
        get
        {
            return stackSize;
        }
    }

    public slotScript MySlot 
    {
        get 
        { 
            return slot; 
        }

        set
        {
            slot = value;
        }
    }

    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }

}
