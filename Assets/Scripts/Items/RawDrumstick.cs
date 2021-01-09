using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RawDrumstick", menuName = "Items/RawDrumstick", order = 4)]
public class RawDrumstick : Item, IUseable
{
    [SerializeField]
    private int damagedHealth;

    public void Use()
    {
        Remove();

        Player.MyInstance.MyHealth.myCurrentValue -= damagedHealth;
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>Don't eat it raw!</color>");
    }


}
