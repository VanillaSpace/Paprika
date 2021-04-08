using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : MonoBehaviour
{
     public virtual bool OnApply()
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }
}
