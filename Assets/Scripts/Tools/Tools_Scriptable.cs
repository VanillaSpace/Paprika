using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tools", menuName = "Data/Tools")]
public class Tools_Scriptable : Item , IUseable
{
    [SerializeField]private ToolAction onAction;

    public ToolAction OnAction { get => onAction; set => onAction = value; }

    public void Use()
    {
        Debug.Log("its a tool!");
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>Use it to chop something!</color>");
    }
}
