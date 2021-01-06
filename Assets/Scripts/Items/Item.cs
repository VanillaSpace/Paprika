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

    private slotScript slot;


    public Sprite MyIcon 
    {
        get
        { 
            return icon; 
        }
    
    }

    public int MyStackSize 
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

    public Quality MyQuality { get => quality; }
    public string MyTitle { get => title; }

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
