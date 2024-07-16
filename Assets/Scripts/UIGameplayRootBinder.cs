using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayRootBinder : MonoBehaviour
{
    public event Action OnBackToMenuButtonClicked;


    public void HandleGoToMenuButtonClicked()
    {
        OnBackToMenuButtonClicked?.Invoke();
    }
}
