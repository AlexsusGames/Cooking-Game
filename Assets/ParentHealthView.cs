using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ParentHealthView : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [Inject] private FamilyStateManager familyStateManager;

    private void Start()
    {
        if (familyStateManager.IsHasParent)
        {
            float fillAmount = (float)familyStateManager.GetParentHealth() / 100;
            barImage.fillAmount = fillAmount;
        }
    }
}
