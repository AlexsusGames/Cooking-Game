using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSlotMachine : MonoBehaviour
{
    [SerializeField] private GameObject slotMachine;
    [SerializeField] private RecipeSlot[] slots;
    [SerializeField] private RecipeSlot winningSlot;

    [SerializeField] private Color[] rarityColors;

    public void SetData(RecipeConfig[] recipes, RecipeConfig openedRecipe)
    {
        slotMachine.SetActive(true);

        winningSlot.SetData(openedRecipe.picture, rarityColors[GetIndexByRatity(openedRecipe.RarityIndex)]);

        for (int i = 0; i < slots.Length; i++)
        {
            var randomRecipe = recipes[Random.Range(0, recipes.Length)];

            int colorIndex = GetIndexByRatity(randomRecipe.RarityIndex);

            slots[i].SetData(randomRecipe.picture, rarityColors[colorIndex]);
        }
    }

    public void DisableSlotMachine() => slotMachine.SetActive(false);

    private int GetIndexByRatity(float rarity)
    {
        int colorIndex = 3;

        if (rarity < 1.5f)
        {
            colorIndex = 0;
        }
        else if (rarity < 1.7)
        {
            colorIndex = 1;
        }
        else if (rarity < 2)
        {
            colorIndex = 2;
        }

        return colorIndex;
    }
}
