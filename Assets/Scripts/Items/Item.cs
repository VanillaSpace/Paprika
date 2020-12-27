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


    public Sprite Icon 
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

    protected slotScript Slot 
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


}
