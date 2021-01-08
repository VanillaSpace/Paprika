using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RawDrumstick", menuName = "Items/RawDrumstick", order = 4)]
public class RawDrumstick : Item
{

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>Raw Chicken meat.</color>");
    }
}
