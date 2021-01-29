using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType { meats, fruits, veggie, dishes, drinks, plants, desserts }

[CreateAssetMenu(fileName = "Food", menuName = "Items/Food", order = 2)]
public class Food : Item,  IUseable
{
    [SerializeField]
    private FoodType foodType;
    public FoodType MyFoodType
    {
        get { return foodType; }
        set { foodType = value; }
    }

    [SerializeField]
    private int gainStamina;
    public int MyGainStamina
    {
        get { return gainStamina; }
        set { gainStamina = value; }
    }
    [SerializeField]
    private int loseStamina;
    public int MyLoseStamina
    {
        get { return loseStamina; }
        set { loseStamina = value; }
    }
    [SerializeField]
    private int DefensePower;
    public int MyDefensePower
    {
        get { return DefensePower; }
        set { DefensePower = value; }
    }
    [SerializeField]
    private int AttackPower;
    public int MyAttackPower
    {
        get { return AttackPower; }
        set { AttackPower = value; }
    }
    [SerializeField]
    private int gainHP;
    public int MyGainHP
    {
        get { return gainHP; }
        set { gainHP = value; }
    }
    [SerializeField]
    private int loseHP;
    public int MyLoseHP
    {
        get { return loseHP; }
        set { loseHP = value; }
    }


    public override string GetDescription()
    {
        string stats = string.Empty;

        //gain
        if (MyAttackPower > 0)
        {
            stats += string.Format("\n +{0} AttackPower", MyAttackPower);
        }
        if (MyDefensePower > 0)
        {
            stats += string.Format("\n +{0} DefensePower", MyDefensePower);
        }
        if (MyGainStamina > 0)
        {
            stats += string.Format("\n +{0} stamina", MyGainStamina);
        }
        if (MyGainHP > 0)
        {
            stats += string.Format("\n +{0} recover HP", MyGainHP);
        }

        //lose
        if (MyLoseStamina > 0)
        {
            stats += string.Format("\n -{0} lose Stamina ", MyLoseStamina);
        }
        if (MyLoseHP > 0)
        {
            stats += string.Format("\n -{0} lose HP", MyLoseHP);
        }


        return base.GetDescription() + stats;
    }

    public void Use()
    {
        Remove();

        //gain
        Player.MyInstance.MyHealth.myCurrentValue += MyGainHP;
        Player.MyInstance.MyStamina.myCurrentValue += MyGainStamina;

        //lose
        Player.MyInstance.MyHealth.myCurrentValue -= MyLoseHP;
        Player.MyInstance.MyStamina.myCurrentValue -= MyLoseStamina;
    }
}
