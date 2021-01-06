using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FoodType { meats, fruits, veggie, dishes, drinks, plants, desserts }

[CreateAssetMenu(fileName = "Food", menuName = "Items/Food", order = 2)]
public class Food : Item,  IUseable
{
    [SerializeField]
    private FoodType foodType;

    [SerializeField]
    private int gainStamina;

    [SerializeField]
    private int loseStamina;

    [SerializeField]
    private int DefensePower;

    [SerializeField]
    private int AttackPower;

    [SerializeField]
    private int gainHP;

    [SerializeField]
    private int loseHP;



    public override string GetDescription()
    {
        string stats = string.Empty;

        //gain
        if (AttackPower > 0)
        {
            stats += string.Format("\n +{0} AttackPower", AttackPower);
        }
        if (DefensePower > 0)
        {
            stats += string.Format("\n +{0} DefensePower", DefensePower);
        }
        if (gainStamina > 0)
        {
            stats += string.Format("\n +{0} stamina", gainStamina);
        }
        if (gainHP > 0)
        {
            stats += string.Format("\n +{0} recover HP", gainHP);
        }

        //lose
        if (loseStamina > 0)
        {
            stats += string.Format("\n -{0} lose Stamina ", loseStamina);
        }
        if (loseHP > 0)
        {
            stats += string.Format("\n -{0} lose HP", loseHP);
        }


        return base.GetDescription() + stats;
    }

    public void Use()
    {
        Remove();

        //gain
        Player.MyInstance.MyHealth.myCurrentValue += gainHP;
        Player.MyInstance.MyStamina.myCurrentValue += gainStamina;

        //lose
        Player.MyInstance.MyHealth.myCurrentValue -= loseHP;
        Player.MyInstance.MyStamina.myCurrentValue -= loseStamina;
    }
}
