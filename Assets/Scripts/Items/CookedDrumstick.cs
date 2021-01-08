using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CookedDrumstick", menuName = "Items/CookedDrumstick", order = 3)]
public class CookedDrumstick : Item
{

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>Cooked Food!</color>");
    }
}
