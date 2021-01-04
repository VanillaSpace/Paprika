using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dumbass", menuName = "Items/Potato", order = 1)]
public class David : Item, IUseable
{

    [SerializeField]
    private int dumbAssLevel;

    public void Use()
    {
        Remove();
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>Oopsie, tee hee haha I'm that betch. \nMy dumb ass level is {0}</color>",dumbAssLevel);
    }
}
