using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneEnterParams 
{
    public string SceneName { get; }

    public SceneEnterParams(string sceneName)
    {
        SceneName = sceneName;
    }

    public T As<T>() where T : SceneEnterParams
    {
        return (T)this;
    }
}
