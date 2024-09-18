using System.Collections.Generic;

[System.Serializable]
public class KitchenUpgradeData 
{
    public List<UpgradeData> Upgrades = new();
}
[System.Serializable]
public class UpgradeData
{
    public InteractivePlaces Type;
    public int Level;

    public UpgradeData(InteractivePlaces type)
    {
        Type = type;
    }
}

