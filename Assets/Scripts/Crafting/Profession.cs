using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profession : MonoBehaviour
{
    private Recipe selectedRecipe;

    public void ShowDescription(Recipe recipe)
    {
        if (selectedRecipe != null)
        {
            selectedRecipe.DeSelect();
        }

        this.selectedRecipe = recipe;

        this.selectedRecipe.Select();
 
    }
}
