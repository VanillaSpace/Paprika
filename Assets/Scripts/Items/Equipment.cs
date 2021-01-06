using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EquipType { hat, chest, gloves, pants, feet, mainHand, offHand, twoHand }

[CreateAssetMenu(fileName = "Equipment", menuName = "Items/Equipment", order = 2)]
public class Equipment : Item
{
    [SerializeField]
    private EquipType equipType;

    [SerializeField]
    private int stamina;

    [SerializeField]
    private int strength;

    [SerializeField]
    private int intellegence;


    
    public override string GetDescription()
    {
        string stats = string.Empty;

        if (intellegence > 0)
        {
            stats += string.Format("\n +{0} intellegence", intellegence);
        }
        if (strength > 0)
        {
            stats += string.Format("\n +{0} strength", strength);
        }
        if (stamina > 0)
        {
            stats += string.Format("\n +{0} stamina", stamina);
        }

        return base.GetDescription();
    }
}
