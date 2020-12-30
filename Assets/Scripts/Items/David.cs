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

        Debug.Log("Oopsie, tee hee haha I'm that betch");
    }
}
