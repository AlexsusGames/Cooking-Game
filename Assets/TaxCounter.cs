using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TaxCounter 
{
    public static float Taxes;
    public static float IncomeTaxes => Income / 100;
    public static int Income;
    public static int PeopleServed;

    public static void Reset()
    {
        PeopleServed = 0;
        Taxes = 0;
        Income = 0;
    }

    public static int GetTaxes()
    {
        return (int)Taxes + (int)IncomeTaxes;
    }
}
