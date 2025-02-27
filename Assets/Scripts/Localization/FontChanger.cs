using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class FontChanger : MonoBehaviour
{
    [SerializeField] private bool isBook;
    [Inject] private LanguageChanger languageChanger;
    private TMP_Text text;

    private void Awake()
    {
        ChangeFont(languageChanger.RegionIndex);
        languageChanger.OnLocalizationChange += ChangeFont;
    }

    private void OnDestroy() => languageChanger.OnLocalizationChange -= ChangeFont;

    private void ChangeFont(int index)
    {
        text = GetComponent<TMP_Text>();
        text.font = languageChanger.GetFont(index, isBook);
    }
}
