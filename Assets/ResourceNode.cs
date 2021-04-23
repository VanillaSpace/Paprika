using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject resource_drop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;
    [SerializeField] ResourceNodeType nodeType;
    [SerializeField] GameObject fishingMiniGame;

    private static ResourceNode instance;

    public static ResourceNode MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ResourceNode>();
            }
            return instance;
        }
    }

    public override void Hit()
    {
        if(nodeType == ResourceNodeType.Water)
        {
            Debug.Log("Spawn Fishing Mini Game!");
            Instantiate(fishingMiniGame, GameObject.FindGameObjectWithTag("Fishing").transform);
        }
        else
        {
            SpawnResource();
        }

        Destroy(gameObject);
    }

    public void SpawnResource()
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 position = transform.position;
            position.x += spread * Random.value - spread / 2;
            position.y += spread * Random.value - spread / 2;
            position.z = 0f;

            GameObject go = Instantiate(resource_drop);
            go.transform.position = position;
        }
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
