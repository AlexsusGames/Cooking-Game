using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KnownRecipesData 
{
    public List<RecipeData> Recipes = new List<RecipeData>();
}
[Serializable] 
public class RecipeData
{
    public string Name;
    public bool IsSelling;
}
