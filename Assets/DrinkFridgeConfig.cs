using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/DrinkFridge", fileName = "Fridge")]
public class DrinkFridgeConfig : UnmovableDeviceConfig
{
    public DrinkType DrinkType;
    public int RequiredRating;
}
