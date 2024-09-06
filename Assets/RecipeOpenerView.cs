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
    private RecipeOpener recipeOpener = new();
    private FoodConfigFinder foodConfigFinder = new();

    private void OnEnable()
    {
        string openedRecipe;
        if(recipeOpener.TryOpen(out openedRecipe))
        {
            recipeImage.sprite = foodConfigFinder.GetRecipeByName(openedRecipe).picture;
            StartCoroutine(Animation());
        }
        else
        {
            fillArea.fillAmount = 1;
            progressText.text = "Вы открыли все рецепты!";
        }
    }

    private IEnumerator Animation()
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
                remainedCount -= 20;
                recipeImage.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                recipeImage.gameObject.SetActive(false);

            }

            todayServed--;
            remainedCount++;

            yield return new WaitForSeconds(0.2f);
        }
        while (todayServed >= 0);
    }
}
