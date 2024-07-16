using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRootView : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Transform uiSceneContainer;

    private void Awake()
    {
        HideLoadingScreen();
    }

    public void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }
    public void HideLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }

    public void AttachSceneUI(GameObject sceneUI)
    {
        CleaerSceneUI();

        sceneUI.transform.SetParent(uiSceneContainer, false);
    }

    private void CleaerSceneUI()
    {
        var childCount = uiSceneContainer.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(uiSceneContainer.GetChild(i).gameObject);
        }
    }
}
