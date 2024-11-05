using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILocalixation : MonoLocalization
{
    private TMP_Text text;

    public override string[] Get()
    {
        text = GetComponent<TMP_Text>();

        if (CachedKeys == null)
        {
            CachedKeys = new string[] { text.text };
        }

        return CachedKeys;
    }

    public override void Set(params string[] param)
    {
        text.text = param[0];
    }
}
