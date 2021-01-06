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

    //public void OnHealthChanged(float health)
    //{
    //    if (OnHealthChanged != null)
    //    {
    //        OnHealthChanged(health);
    //    }
    //}

    //public void OnCharacterRemoved()
    //{
    //    if (OnCharacterRemoved != null)
    //    {
    //        CharacterRemoved();
    //    }

    //    Destroy(gameObject);
    //}

    public virtual void Interact()
    {
        Debug.Log("dialogue NPC");
    }
}
