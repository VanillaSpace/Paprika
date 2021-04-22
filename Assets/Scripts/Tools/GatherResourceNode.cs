using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined, 
    Tree, 
    Water
}

[CreateAssetMenu(menuName = "Data/Tool action/Gather Resrouce Node")]
public class GatherResourceNode : ToolAction
{
   [SerializeField] float sizeOfInteractableArea = 1f;
   [SerializeField] List<ResourceNodeType> canHitNodesOfType;

   public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                if(hit.CanBeHit(canHitNodesOfType) == true)
                {
                    hit.Hit();
                    return true;
                }
            }
        }
        return false;
    }
}
