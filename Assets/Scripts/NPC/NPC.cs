using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    public virtual void Deselect()
    {

    }

    public virtual Transform Select()
    {
        return hitBox;
    }
}
