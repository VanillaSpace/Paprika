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

        Player.MyInstance.MyHealth.myCurrentValue += recoveredHealth;
        
    }


}
