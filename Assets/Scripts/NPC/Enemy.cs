using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    public override Transform Select()
    {
        healthGroup.alpha = 1;

        return base.Select();
    }

    public override void Deselect()
    {
        healthGroup.alpha = 0;

        base.Deselect();
    }

}
