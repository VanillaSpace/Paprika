using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="HealthPotion",menuName ="Items/Potion",order = 1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField]
    private int recoveredHealth;

    public void Use()
    {
        Remove();

        Player.MyInstance.GetHealth(recoveredHealth);

    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>USE: RESTORES {0} HEALTH</color>", recoveredHealth);
    }
}
