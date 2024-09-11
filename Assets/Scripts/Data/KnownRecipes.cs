using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnownRecipes 
{
    private const string Key = "KnownRecipes";
    private const int MaxCountOfSellingRecipes = 24;
    private KnownRecipesData data;

    private void SaveData()
    {
        string save = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(Key, save);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(Key))
        {
            string save = PlayerPrefs.GetString(Key);
            data = JsonUtility.FromJson<KnownRecipesData>(save);
        }
        else CreateData();
    }

    private void CreateData()
    {
        data = new KnownRecipesData();
        data.Recipes.Add(new RecipeData { Name = "Кофе" });
        data.Recipes.Add(new RecipeData { Name = "Шоколадный пончик" });
        data.Recipes.Add(new RecipeData { Name = "Клубнийчный пончик" });
        data.Recipes.Add(new RecipeData { Name = "Тост с апельсином" });
        SaveData();
    }

    public void AddRecipe(string name)
    {
        LoadData();

        if (!Has(name))
        {
            data.Recipes.Add(new RecipeData { Name = name });
        }

        SaveData();
    }

    public bool Has(string name)
    {
        for (int i = 0; i < data.Recipes.Count; i++)
        {
            if (data.Recipes[i].Name == name) return true;
        }

        return false;
    }

    public bool ChangeSelling(string name, bool value)
    {
        if (GetCountOfSellingRecipes() >= MaxCountOfSellingRecipes)
        {
            if (value)
            {
                return false;
            }
        }

        for (int i = 0; i < data.Recipes.Count; i++)
        {
            if (data.Recipes[i].Name == name)
            {
                data.Recipes[i].IsSelling = value;
            }
        }

        SaveData();
        return true;
    }

    public List<string> GetAvailableRecipes()
    {
        LoadData();
        var list = new List<string>();

        for (int i = 0; i < data.Recipes.Count; i++)
        {
            list.Add(data.Recipes[i].Name);
        }

        return list;
    }

    public bool IsSelling(string name)
    {
        LoadData();

        for (int i = 0; i < data.Recipes.Count; i++)
        {
            if (data.Recipes[i].Name == name)
            {
                return data.Recipes[i].IsSelling;
            }
        }

        return false;
    }

    public bool IsAvailable(string name)
    {
        LoadData();

        for (int i = 0; i < data.Recipes.Count; i++)
        {
            if (data.Recipes[i].Name == name)
            {
                return true;
            }
        }

        return false;
    }

    public string GetRandomSellingRecipe()
    {
        var list = GetAvailableRecipes();

        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(0, list.Count);
            if (IsSelling(list[random])) return list[random];
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (IsSelling(list[i]))
            {
                return list[i];
            }
        }

        return null;
    }

    public int GetCountOfSellingRecipes()
    {
        var list = GetAvailableRecipes();
        int count = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (IsSelling(list[i]))
            {
                count++;
            }
        }

        return count;
    }

    public int GetCountOfAvailableRecipes()
    {
        var list = GetAvailableRecipes();

        return list.Count;
    }
}
