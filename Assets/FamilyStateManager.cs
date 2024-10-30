using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FamilyStateManager : IProgressDataProvider
{
    private const string G_HEALTH_SAVE = "Girl_Health";
    private const string P_HEALTH_SAVE = "Parent_Health";
    private const int PARENT_STANDART_HEALTH = 100;
    private const int GIRL_STANDART_HEALTH = 3;

    private int parentHealth;
    private int girlHealth;
    public bool IsFed { get; set; }
    public bool IsHasParent => parentHealth > 0;
    public bool IsHasGirl => girlHealth > 0;

    public void Load()
    {
        if (PlayerPrefs.HasKey(P_HEALTH_SAVE))
        {
            parentHealth = PlayerPrefs.GetInt(P_HEALTH_SAVE);
        }
        else parentHealth = PARENT_STANDART_HEALTH;

        if (PlayerPrefs.HasKey(G_HEALTH_SAVE))
        {
            girlHealth = PlayerPrefs.GetInt(G_HEALTH_SAVE);
        }
        else girlHealth = GIRL_STANDART_HEALTH;
    }

    public void Save()
    {
        if (parentHealth > 0) parentHealth -= 10;
        if (!IsFed && IsHasParent) girlHealth--;
        PlayerPrefs.SetInt(G_HEALTH_SAVE, girlHealth);
        PlayerPrefs.SetInt(P_HEALTH_SAVE, parentHealth);
    }

    public void EndStory()
    {
        parentHealth = 100000;
        girlHealth = 0;
    }
    public int GetParentHealth() => parentHealth;
    public int GetGirlHealth() => girlHealth;
}
