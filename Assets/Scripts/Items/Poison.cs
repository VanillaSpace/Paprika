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
}
