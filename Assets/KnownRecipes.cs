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
        data.Recipes.Add(new RecipeData { Name = "����� ����" });
        data.Recipes.Add(new RecipeData { Name = "���������� ������" });
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
}