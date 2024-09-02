using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdviceLine : MonoBehaviour
{
    [SerializeField] private TMP_Text adviceText;
    private Coroutine coroutine;

    public void ShowAdvice(string text)
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(Timer(text));
        }
    }

    public IEnumerator Timer(string text)
    {
        adviceText.gameObject.SetActive(true);
        adviceText.text = text;
        yield return new WaitForSeconds(3);
        adviceText.gameObject.SetActive(false);
        coroutine = null;
    }
}
