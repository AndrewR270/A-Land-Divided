using UnityEngine;
using System.Collections.Generic;

public class Province
{
    // Identifiers
    public string ProvinceID;

    public string Name => ProvinceTextLoader.Provinces[ProvinceID].name;
    public string PeriodName => ProvinceTextLoader.Provinces[ProvinceID].period_name;
    public string Pronunciation => ProvinceTextLoader.Provinces[ProvinceID].pronunciation;
    public string Description => ProvinceTextLoader.Provinces[ProvinceID].description;

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

    // Stability
    public int Stability;

    // Contingents raised from this province
    public List<Contingent> Contingents = new List<Contingent>();
}
