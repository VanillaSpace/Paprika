using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality {Common, Rare, Epic, Legendary}

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

    public virtual string GetDescription()
    {
        string color = string.Empty;

        switch (quality)
        {
            case Quality.Common:
                color = "#E7E7E7";
                break;
            case Quality.Rare:
                color = "#0669B5";
                break;
            case Quality.Epic:
                color = "#8006B4";
                break;
            case Quality.Legendary:
                color = "#B48006";
                break;
        }

        return string.Format("<color={0}>{1}</color>", color, title);
    }

    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }

}
