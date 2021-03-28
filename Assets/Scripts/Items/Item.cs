using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    //scripObj = does not need game obj

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;
    public Quality MyQuality
    {
        get { return quality; }
        set { quality = value; }
    }
    private SlotScript slot;


    public Sprite MyIcon 
    {
        get
        { 
            return icon; 
        }
        set
        {
            icon = value;

        }
    
    }

    public int MyStackSize 
    {
        get
        {
            return stackSize;
        }

        set
        {
            stackSize = value;
        }
    }

    public SlotScript MySlot 
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


    public string MyTitle { get => title; set
        {
            title = value;
        }
    }

    public virtual string GetDescription()
    {
      return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[MyQuality],  MyTitle);
    }

    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }

}
