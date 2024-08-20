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
    private RecipeConfig[] recipes;
    private int index = 0;

    private void Awake() => recipes = foodConfigFinder.GetAllRecipes();
    private void OnEnable()
    {
        CreateMenu();
        
        pageNumbers.Clear();
        for (int i = 0; i < recipes.Length; i++)
        {
            if(i % 2 == 0) pageNumbers.Add(i);
        }

        openedBook.SetActive(false);
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
        firstPage.SetData(recipes[index], true);

        if(index + 1 < recipes.Length)
        {
            secondPage.gameObject.SetActive(true);
            secondPage.SetData(recipes[index + 1], true);
        }
        else secondPage.gameObject.SetActive(false);
    }

    private void CreateMenu()
    {
        for (int i = 0; i < menuPositions.Length; i++)
        {
            menuPositions[i].enabled = true;
            menuPositions[i].sprite = recipes[i].picture;
        }
    }
}
