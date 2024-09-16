using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeShopView[] upgradesView;
    [SerializeField] private int[] prices;
    [SerializeField] private Bank bank;
    private KitchenUpgradeProvider dataProvider;

    public void Init(KitchenUpgradeProvider provider)
    {
        dataProvider = provider;

        var data = dataProvider.GetUpgrades();

        for (int i = 0; i < upgradesView.Length; i++)
        {
            int index = i;
            var level = data[upgradesView[index].Type];

            upgradesView[index].UpdateView(level, prices[level]);
            upgradesView[index].GetButton().onClick.AddListener(() => Upgrade(upgradesView[index].Type));
        }
    }

    public void Upgrade(InteractivePlaces type)
    {
        var data = dataProvider.GetUpgrades();
        int currentLevel = data[type];

        if(currentLevel < prices.Length)
        {
            if (bank.Has(prices[currentLevel]))
            {
                dataProvider.ChangeLevel(type);
                bank.Change(-prices[currentLevel]);

                for (int i = 0; i < upgradesView.Length; i++)
                {
                    if (upgradesView[i].Type == type)
                    {
                        int nextLevel = ++currentLevel;
                        upgradesView[i].UpdateView(nextLevel, prices[nextLevel]);
                    }
                }
            }
        }
    }
}
