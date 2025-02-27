using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderView : MonoBehaviour
{
    [SerializeField] private GameObject[] orderImage;
    [SerializeField] private Image[] foodImage;
    [SerializeField] private Image waitingBar;
    [SerializeField] private Color[] barColors;

    public void ShowClientOrder(List<RecipeConfig> configs)
    {
        UpdateView(configs);
        waitingBar.fillAmount = 1;
    }

    public void UpdateView(List<RecipeConfig> configs)
    {
        ImagesEnabled(false);

        if(configs != null)
        {
            for (int i = 0; i < configs.Count; i++)
            {
                orderImage[i].SetActive(true);
                foodImage[i].sprite = configs[i].picture;
            }
        }
    }

    private void ImagesEnabled(bool value)
    {
        for (int i = 0;i < foodImage.Length; i++)
        {
            orderImage[i].SetActive(value);
        }
    }

    public void ChangePatience(float amount)
    {
        waitingBar.fillAmount = amount;

        if (amount > 0.5) waitingBar.color = barColors[0];
        else if(amount > 0.2) waitingBar.color = barColors[1];
        else waitingBar.color = barColors[2];

        if(amount < 0)
        {
            ImagesEnabled(false);
        }
    }
}
