using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text taxes;
    [SerializeField] private TMP_Text incomeTaxes;
    [SerializeField] private TMP_Text all;
    [SerializeField] private Button button;

    public void Open()
    {
        gameObject.SetActive(true);
        taxes.text = $"{(int)TaxCounter.Taxes}$";
        incomeTaxes.text = $"{(int)TaxCounter.IncomeTaxes}$";
        all.text = $"{(int)TaxCounter.Taxes + (int)TaxCounter.IncomeTaxes}$";
        button.onClick.AddListener(() => SceneManager.LoadScene(0));
        Cursor.visible = true;
    }
}