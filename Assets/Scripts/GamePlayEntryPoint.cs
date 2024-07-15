using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject sceneRootBinder;

    public void Run()
    {
        Debug.Log("Gameplay scene loaded");
    }
}
