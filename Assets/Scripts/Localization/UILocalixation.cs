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
        return new string[] { text.text };
    }

    public override void Set(params string[] param)
    {
        text.text = param[0];
    }

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
}
