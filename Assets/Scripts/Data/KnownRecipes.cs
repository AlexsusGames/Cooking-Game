using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnownRecipes 
{
    private const string Key = "KnownRecipes";
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
        data.Recipes.Add(new RecipeData { Name = "Шоколадный пончик" });
        data.Recipes.Add(new RecipeData { Name = "Клубнийчный пончик" });
        data.Recipes.Add(new RecipeData { Name = "Тост с яйцом и колбасой" });
        data.Recipes.Add(new RecipeData { Name = "Тост с апельсином" });
        SaveData();
    }

    public void AddRecipe(string name)
    {
        LoadData();
        data.Recipes.Add(new RecipeData { Name = name});
        SaveData();
    }

    public void ChangeSelling(string name, bool value)
    {
        LoadData();

        for (int i = 0; i < data.Recipes.Count; i++)
        {
            if(data.Recipes[i].Name == name)
            {
                data.Recipes[i].IsSelling = value;
            }
        }

        SaveData();
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

        throw new System.Exception($"Name: {name} isn't found");
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
            if (IsSelling(list[i])) count++;
        }

        return count;
    }

    public int GetCountOfAvailableRecipes()
    {
        var list = GetAvailableRecipes();

        return list.Count;
    }
}
