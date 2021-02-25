using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources", menuName = "Items/Resources", order = 5)]
public class Resource_Items : Item, IUseable
{

    public void Use()
    {
        //Remove();

        Debug.Log("Used for crafting, not eating.");
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>Use it to craft something!</color>");
    }


}