using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingBook : MonoBehaviour
{
    [SerializeField] private Transform spawnPointA;
    [SerializeField] private Transform spawnPointB;
    [SerializeField] private RecipeView recipe;


    [SerializeField] private RecipeConfig recipeConfigA;
    [SerializeField] private RecipeConfig recipeConfigB;

    private void Awake()
    {
        var objA = Instantiate(recipe.gameObject, spawnPointA);
        var objB = Instantiate(recipe.gameObject, spawnPointB);
        objA.TryGetComponent(out RecipeView recipeViewA);
        objB.TryGetComponent(out RecipeView recipeViewB);
        recipeViewA.SetData(recipeConfigA);
        recipeViewB.SetData(recipeConfigB);
    }
}
