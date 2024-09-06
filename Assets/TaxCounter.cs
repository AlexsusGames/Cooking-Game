using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TaxCounter 
{
    public static float Taxes;
    public static float IncomeTaxes;
    public static int PeopleServed;

    public static void Reset()
    {
        PeopleServed = 0;
        Taxes = 0;
        IncomeTaxes = 0;
    }

    public static void OnServed(float tax)
    {
        IncomeTaxes += tax;
        PeopleServed++;
    }
}
