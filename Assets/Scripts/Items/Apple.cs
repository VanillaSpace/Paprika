using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Apples", menuName = "Items/Apple", order = 1)]
public class Apple : Item, IUseable
{
    [SerializeField]
    private int recoveredHealth;

    public void Use()
    {
        Remove();

        Player.MyInstance.MyHealth.myCurrentValue += recoveredHealth;
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>USE: RESTORES {0} HEALTH</color>", recoveredHealth);
    }
}
