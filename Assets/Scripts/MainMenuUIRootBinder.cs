using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenuUIRootBinder : MonoBehaviour
{
    public event Action OnGoToGamePlayButtonClicked;


    public void HandleGoToGamePlayButtonClicked()
    {
        OnGoToGamePlayButtonClicked?.Invoke();
    }
}
