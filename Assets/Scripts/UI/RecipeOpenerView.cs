using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeOpenerView : MonoBehaviour
{
    [SerializeField] private Image fillArea;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Image recipeImage;

    [SerializeField] private RecipeSlotMachine recipeSlotMachine;
    [SerializeField] private float animationDuration;

    private RecipeOpener recipeOpener = new();
    private FoodConfigFinder foodConfigFinder = new();
    private List<RecipeConfig> recipeConfigList = new();

    private void OnEnable()
    {
        recipeConfigList = foodConfigFinder.GetUnAvailableRecipes();

        string openedRecipe;
        if(recipeOpener.TryOpen(out openedRecipe))
        {

            if (!string.IsNullOrEmpty(openedRecipe))
            {
                recipeImage.sprite = foodConfigFinder.GetRecipeByName(openedRecipe).picture;
            }

            RecipeConfig config = foodConfigFinder.GetRecipeByName(openedRecipe);
            StartCoroutine(Animation(config));
        }
        else
        {
            fillArea.fillAmount = 1;
            progressText.text = "Вы открыли все рецепты!";
        }
    }

    private IEnumerator Animation(RecipeConfig openedRecipe)
    {
        int todayServed = TaxCounter.PeopleServed;
        int remainedCount = recipeOpener.CatchedPeopleCount - todayServed;

        while(remainedCount > 20)
        {
            remainedCount -= 20;
            yield return null;
        }

        do
        {
            progressText.text = $"{remainedCount}/20";
            float fillAmount = (float)remainedCount / 20;
            fillArea.fillAmount = fillAmount;

            if (remainedCount == 20)
            {
                recipeSlotMachine.SetData(recipeConfigList.ToArray(), openedRecipe);
                yield return new WaitForSeconds(animationDuration);
                recipeSlotMachine.DisableSlotMachine();

                remainedCount -= 20;
                recipeImage.gameObject.SetActive(true);
                yield return new WaitForSeconds(2);
                recipeImage.gameObject.SetActive(false);
            }

            todayServed--;
            remainedCount++;

            yield return new WaitForSeconds(0.2f);
        }
        while (todayServed >= 0);
    }
}
