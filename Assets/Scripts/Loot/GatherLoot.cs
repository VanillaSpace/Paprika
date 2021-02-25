using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherLoot : LootTable, IInteractable
{
    [SerializeField]
    public SpriteRenderer spriteRender;

    [SerializeField]
    private Sprite defaultSprite;

    [SerializeField]
    private Sprite gatherSprite;

    [SerializeField]
    private GameObject gatherIndicator;

    //chopping the tree
    public bool canChop = false;

    [SerializeField]
    private Resource resource;

    private static GatherLoot instance;

    public static GatherLoot MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GatherLoot>();
            }
            return instance;
        }
    }


    private void Start()
    {
        RollLoot();
     
    }
    public void Update()
    {

    }
    protected override void RollLoot()
    {
        MyDroppedItems = new List<Drop>();

        foreach (Loot l in loot)
        {
            int roll = Random.Range(0, 100);

            if (roll <= l.MyDropChance)
            {
                int itemCount = Random.Range(1, 6);

                for (int i = 0; i < itemCount; i++)
                {

                    MyDroppedItems.Add(new Drop(Instantiate(l.MyItem), this));
                }

                spriteRender.sprite = gatherSprite;
                gatherIndicator.SetActive(true);
                canChop = false;
            }
            else
            {
                //if there are no fruits , Paprika can chop
                gameObject.SetActive(true);
                gatherIndicator.SetActive(false);
                canChop = true;

            }
        }

    }

    public void Interact()
    {
        if (canChop)
        {
            resource.Interact();
        }
        else
        {

          BasicMovement.MyInstance.Gather(projectileBook.MyInstance.GetProjectile("Gather"), MyDroppedItems);
          LootWindow.MyInstance.MyInteractable = this;
        }
    }


    public void StopInteract()
    {
        LootWindow.MyInstance.MyInteractable = null;

        if (MyDroppedItems.Count == 0)
        {
            spriteRender.sprite = defaultSprite;
            gameObject.SetActive(true);
            gatherIndicator.SetActive(false);
            canChop = true;
        }

        LootWindow.MyInstance.Close();
    }
}
