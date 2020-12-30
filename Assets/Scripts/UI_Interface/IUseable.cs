using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


interface IUseable
{
    Sprite MyIcon
    {
        get;
    }

    void Use();
}
