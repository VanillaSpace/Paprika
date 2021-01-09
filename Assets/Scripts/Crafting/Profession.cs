using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profession : MonoBehaviour
{

    [SerializeField]
    private Text title;

    [SerializeField]
    private Text description;

    [SerializeField]
    private GameObject materialPrefab;

    [SerializeField]
    private Transform parent;

    private List<GameObject> materials = new List<GameObject>();

    [SerializeField]
    private Recipe selectedRecipe;

    [SerializeField]
    private Text countTxt;

    [SerializeField]
    private ItemInfo craftItemInfo;

    private int maxAmmount;

    private int amount;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Start()
    {
        inventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateMaterialCount);
        ShowDescription(selectedRecipe);
    }

    public void ShowDescription(Recipe recipe)
    {
        if (selectedRecipe != null)
        {
            selectedRecipe.DeSelect();
        }

        this.selectedRecipe = recipe;

        this.selectedRecipe.Select();

        foreach (GameObject gameObject in materials)
        {
            Destroy(gameObject);
        }

        materials.Clear();

        title.text = recipe.Output.MyTitle;

        description.text = recipe.MyDescription + " " + recipe.Output.MyTitle;

        craftItemInfo.Initialize(recipe.Output, 1);

        foreach (CraftingMaterial material in recipe.Materials)
        {
            GameObject tmp = Instantiate(materialPrefab, parent);

            tmp.GetComponent<ItemInfo>().Initialize(material.MyItem, material.MyCount);

            materials.Add(tmp);
        }

        UpdateMaterialCount(null);
 
    }

    private void UpdateMaterialCount(Item item)
    {
        foreach (GameObject material in materials)
        {
            ItemInfo tmp = material.GetComponent<ItemInfo>();

            tmp.UpdateStackCount();
        }
    }

    public void Craft()
    {
        if (CanCraft())
        {
            StartCoroutine(CraftRoutine(0));
        }    
        
    }

    private bool CanCraft()
    {
        bool canCraft = true;

        foreach (CraftingMaterial material in selectedRecipe.Materials)
        {
            int count = inventoryScript.MyInstance.GetItemCount(material.MyItem.MyTitle);
            if (count >= material.MyCount)
            {
                continue;
            }
            else
            {
                canCraft = false;
                break;
            }
        }

        return canCraft;
    }


    private IEnumerator CraftRoutine(int count)
    {
        yield return BasicMovement.MyInstance.MyInitRoutine = StartCoroutine(BasicMovement.MyInstance.CraftRoutine(selectedRecipe));
    }

    public void AddItemToInventory()
    {
        if (inventoryScript.MyInstance.AddItem(craftItemInfo.MyItem))
        {
            foreach (CraftingMaterial material in selectedRecipe.Materials)
            {
                for (int i = 0; i < material.MyCount; i++)
                {
                    inventoryScript.MyInstance.RemoveItem(material.MyItem);
                }
            }
        }

        inventoryScript.MyInstance.AddItem(craftItemInfo.MyItem);
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

}
