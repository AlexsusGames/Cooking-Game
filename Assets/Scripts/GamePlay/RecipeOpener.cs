using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeOpener 
{
    private const int PoepleToOpen = 20;

    private KnownRecipes knownRecipes = new();
    private FoodConfigFinder foodConfigFinder = new();
    private PeopleCounter peopleCounter = new();
    public int CatchedPeopleCount { get; private set; }

    public bool TryOpen(out string name)
    {
        if(CatchedPeopleCount == 0)
        {
            CatchedPeopleCount = peopleCounter.GetCount();
        }

        if(peopleCounter.GetCount() >= PoepleToOpen)
        {
            peopleCounter.ChangeCount(-PoepleToOpen);
            string newRecipe = foodConfigFinder.GetRandomRecipeName();

            if (!string.IsNullOrEmpty(newRecipe))
            {
                knownRecipes.AddRecipe(newRecipe);
                knownRecipes.ChangeSelling(newRecipe, false);
                name = newRecipe;
                return true;
            }
            else
            {
                name = null;
                return false;
            }
        }

        if(peopleCounter.GetCount() >= PoepleToOpen)
        {
            return TryOpen(out name);
        }

        name = null;
        return true;
    }
}
