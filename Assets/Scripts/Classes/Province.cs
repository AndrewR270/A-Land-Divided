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

    // Stability
    public int Stability;       // 0–100

    // Contingents raised from this province
    public List<Contingent> Contingents = new List<Contingent>();
}
