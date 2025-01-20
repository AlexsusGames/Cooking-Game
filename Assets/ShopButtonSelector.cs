using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonSelector : MonoBehaviour
{
    [SerializeField] private RectTransform selector;
    [SerializeField] private Button[] buttons;

    public void SelectButton(int index)
    {
        selector.SetParent(buttons[index].transform);
        selector.anchoredPosition = Vector2.zero;
    }
}
