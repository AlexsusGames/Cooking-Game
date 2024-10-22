using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DialogSelector : MonoBehaviour
{
    [SerializeField] private DialogConfig firstPoliceDialog;
    [SerializeField] private DialogConfig secondPoliceDialog;
    [SerializeField] private DialogConfig coffeeMachineDialog;

    [SerializeField] private List<DialogConfig> possibleDialogs;
    [SerializeField] private List<DialogConfig> lateStoryDialogs; 

    [Inject] private FamilyStateManager familyStateManager;
    [Inject] private UpgradeService upgradeService;

    private void CreateVariants()
    {
        if (!familyStateManager.IsHasGirl)
        {
            possibleDialogs.Clear();
            possibleDialogs.Add(firstPoliceDialog);
            possibleDialogs.Add(secondPoliceDialog);
            return;
        }

        if (familyStateManager.IsHasParent)
        {
            if (familyStateManager.GetGirlHealth() < 3)
            {
                possibleDialogs.Insert(0, firstPoliceDialog);
            }

            if (upgradeService.Has(InteractivePlaces.CoffeeMachine))
            {
                possibleDialogs.Insert(0, coffeeMachineDialog);
            }
            return;
        }
        else possibleDialogs = lateStoryDialogs;
    }

    public void SelectDialog(DialogSystem system)
    {
        CreateVariants();

        for (int i = 0; i < possibleDialogs.Count; i++)
        {
            bool isFound = system.TryOpenDialog(possibleDialogs[i]);
            if (isFound) return;
        }
    }
}
