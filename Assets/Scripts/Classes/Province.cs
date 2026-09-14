using UnityEngine;
using System.Collections.Generic;

public class Province
{
    // Identifiers
    public string ProvinceID;

    public string Name => ProvinceText.Text[ProvinceID].name;
    public string PeriodName => ProvinceText.Text[ProvinceID].period_name;
    public string Pronunciation => ProvinceText.Text[ProvinceID].pronunciation;
    public string Description => ProvinceText.Text[ProvinceID].description;

    // Information
    public Faction Owner;
    public List<string> Adjacent;

    // Bonuses
    public Bonus ProvinceBonuses => BonusRegistry.Bonuses[ProvinceID];
    public string BonusName => ProvinceBonuses.name;
    public string BonusBenefit1 => ProvinceBonuses.benefit1;
    public string BonusBenefit2 => ProvinceBonuses.benefit2;



    // Population
    public int HomePopulation;
    public int LeviedPopulation;

    // Food & Growth
    public int Surplus;
    public float GrowthRate;

    // Buildings
    public Farms Farms = new Farms(0);
    public Barracks Barracks = new Barracks(0);
    public Markets Markets = new Markets(0);
    public Port Port = new Port(0);
    public Prestige Prestige = new Prestige(0);


    // Stability
    public int Stability;

    // Contingents raised from this province
    public List<Contingent> Contingents = new List<Contingent>();
}
