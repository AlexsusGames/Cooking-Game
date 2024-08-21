using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingBook : MonoBehaviour
{
    [SerializeField] private Image[] menuPositions;
    [SerializeField] private GameObject openedBook;
    [SerializeField] private GameObject closedBook;

    [SerializeField] private RecipeView firstPage;
    [SerializeField] private RecipeView secondPage;

    private FoodConfigFinder foodConfigFinder = new();
    private List<int> pageNumbers = new List<int>();
    private KnownRecipes knownRecipes = new();
    private RecipeConfig[] recipes;
    private int index = 0;

    private void OnEnable()
    {
        UpdateData();
        CreateMenu();

        openedBook.SetActive(false);

        firstPage.sellingChanged += UpdateMenu;
        secondPage.sellingChanged += UpdateMenu;
    }

    private void OnDisable()
    {
        firstPage.sellingChanged -= UpdateMenu;
        secondPage.sellingChanged -= UpdateMenu;
    }

    private void UpdateMenu(string name, bool isSelling)
    {
        knownRecipes.ChangeSelling(name, isSelling);
        CreateMenu();
    }

    private void UpdateData()
    {
        var availableRecipes = knownRecipes.GetAvailableRecipes();
        var recipesConfig = foodConfigFinder.GetRecipesByName(availableRecipes);
        recipes = recipesConfig.ToArray();

        pageNumbers.Clear();
        for (int i = 0; i < recipes.Length; i++)
        {
            if (i % 2 == 0) pageNumbers.Add(i);
        }
    }

    public void OpenBook()
    {
        openedBook.SetActive(true);
        index = 0;
        SetRecipes(0);
    }

    public void ChangePage(int amount)
    {
        index += amount;
        if (index < 0)
        {
            index = 0;
            openedBook.SetActive(false);
        }
        else if(index >= pageNumbers.Count)  index = pageNumbers.Count - 1;

        SetRecipes(pageNumbers[index]);
    }

    private void SetRecipes(int index)
    {
        firstPage.gameObject.SetActive(true);
        var recipeA = recipes[index];
        int nextPage = index + 1;

        firstPage.SetData(recipeA, knownRecipes.IsSelling(recipeA.Name));

        if(nextPage < recipes.Length)
        {
            secondPage.gameObject.SetActive(true);
            var recipeB = recipes[nextPage];

            secondPage.SetData(recipeB, knownRecipes.IsSelling(recipeB.Name));
        }
        else secondPage.gameObject.SetActive(false);
    }

    private void CreateMenu()
    {
        var sellingRecipes = new List<RecipeConfig>();

        for (int i = 0; i < recipes.Length; i++)
        {
            if (knownRecipes.IsSelling(recipes[i].Name))
            {
                sellingRecipes.Add(recipes[i]);
            }
        }

        for (int i = 0; i < menuPositions.Length; i++)
        {
            if(sellingRecipes.Count > i)
            {
                menuPositions[i].enabled = true;
                menuPositions[i].sprite = sellingRecipes[i].picture;
            }
            else menuPositions[i].enabled = false;
        }
    }
}
