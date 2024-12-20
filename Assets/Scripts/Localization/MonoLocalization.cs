using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoLocalization : MonoBehaviour, ILocalization
{
    protected string[] CachedKeys;
    public abstract string[] Get();
    public abstract void Set(params string[] param);
}
