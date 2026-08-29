using UnityEngine;
using System.Collections.Generic;


public class Province
{
    // Identifiers
    public string ColorID;
    public string Name;
    public string PeriodName;
    public string Pronunciation;

    // Information
    public Faction Owner;
    public List<string> Adjacent;
    public Resource ProvinceResource;

    // Population
    public int HomePopulation;
    public int LeviedPopulation;

    // Food & Growth
    public int Surplus;
    public float GrowthRate;

    // Buildings
    public int FarmLevel;
    public int BarracksLevel;
    public int MarketLevel;
    public int PortLevel;
    public int SpecialBuildingLevel;

    private static readonly int[] costTiers = {0, 200, 400, 900, 1600, 2500, 3600};
    private static readonly int[] timeTiers = {0, 2, 4, 6, 8, 10, 12};


    // Stability
    public int Stability;       // 0–100

    // Contingents raised from this province
    public List<Contingent> Contingents = new List<Contingent>();

}
