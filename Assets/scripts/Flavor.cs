using System;
using UnityEngine;

enum Taste
{
    PERFECT,
    WONDERFUL,
    GOOD,
    BAD,
    TERRIBLE
};

[Serializable]
public class Flavor
{
    [SerializeField] private int spice = 0;
    [SerializeField] private int sugar = 0;
    [SerializeField] private int aroma = 0;

    public Flavor(int spice, int sugar, int aroma)
    {
        this.spice = spice;
        this.sugar = sugar;
        this.aroma = aroma;
    }

    public void AddFlavor (Flavor flavor)
    {
        this.SetSpice(this.GetSpice() + flavor.GetSpice());
        this.SetSugar(this.GetSugar() + flavor.GetSugar());
        this.SetAroma(this.GetAroma() + flavor.GetAroma());
    }

    private int CalculatePerfection (Flavor perfectFlavor)
    {
        int spiceDifference = Math.Abs(perfectFlavor.GetSpice() - this.GetSpice());
        int sugarDifference = Math.Abs(perfectFlavor.GetSugar() - this.GetSugar());
        int aromaDifference = Math.Abs(perfectFlavor.GetAroma() - this.GetAroma());

        return spiceDifference + sugarDifference + aromaDifference;
    }

    private Taste CategorizeTaste (Flavor perfectFlavor)
    {
        int perfection = CalculatePerfection(perfectFlavor);
        
        switch(perfection)
        {
            case 0:
                return Taste.PERFECT;
            case int n when (n > 0 && n <= 2):
                return Taste.WONDERFUL;
            case int n when (n > 2 && n <= 4):
                return Taste.GOOD;
            case int n when (n > 4 && n <= 6):
                return Taste.BAD;
            default:
                return Taste.TERRIBLE;
        }
    }

    public int GetSpice() { return this.spice; }
    private void SetSpice( int spice ) { this.spice = spice; }

    public int GetSugar() { return this.sugar; }
    private void SetSugar( int sugar ) { this.sugar = sugar; }

    public int GetAroma() { return this.aroma; }
    private void SetAroma( int aroma ) { this.aroma = aroma; }
}
