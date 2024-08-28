using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsOrder : MonoBehaviour
{
    public RecipeConfig Order { get; private set; }

    public void Bind(RecipeConfig config)
    {
        Order = config;
    }
}
