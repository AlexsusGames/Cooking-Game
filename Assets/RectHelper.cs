using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectHelper : MonoBehaviour
{
    [SerializeField] private Vector2 targetPosition;

    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = targetPosition;
    }
}
