using UnityEngine;

public class Province
{
    public string Name;
    public string PeriodName;
    public string Pronunciation;

    public Faction Owner;

    // Population
    public int HomePopulation;
    public int LeviedPopulation;

    // Food & Growth
    public int FarmTier;        // determines surplus
    public int Surplus;         // Sprovince = T - A
    public float GrowthRate;    // Gprovince

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

    // Trade resources
    public TradeResource Resource;

    // Map data
    public Color ProvinceColor;
    public List<string> AdjacentProvinces; // names or IDs
}
