using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderView : MonoBehaviour
{
    [SerializeField] private GameObject orderImage;
    [SerializeField] private Image foodImage;
    [SerializeField] private Image waitingBar;

    public void ShowClientOrder(Sprite sprite)
    {
        foodImage.sprite = sprite;
        waitingBar.fillAmount = 1;
    }

    public void ChangePatience(float amount)
    {
        waitingBar.fillAmount = amount;
        orderImage.SetActive(amount > 0);
    }
}
