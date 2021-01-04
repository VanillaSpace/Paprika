using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Items/Poison", order = 1)]
public class Poison : Item, IUseable
{
    [SerializeField]
    private int poisonLevel;

    public void Use()
    {
        Remove();

        Player.MyInstance.MyHealth.myCurrentValue -= poisonLevel;
        Player.MyInstance.MyStamina.myCurrentValue -= poisonLevel;
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>USE: {0} DAMAGE TO HP AND STAMINA</color>", poisonLevel);
    }
}
