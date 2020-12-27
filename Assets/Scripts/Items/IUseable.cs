using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;


interface IUseable
{
    Sprite MyIcon
    {
        get;
    }

    void Use();
}
