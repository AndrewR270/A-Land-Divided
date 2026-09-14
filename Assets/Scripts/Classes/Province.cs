using UnityEngine;
using System.Collections.Generic;

public class Province
{
    public string ProvinceID;
    public List<string> Adjacent;

    // Province Text
    public string Name => ProvinceText.Text[ProvinceID].name;
    public string PeriodName => ProvinceText.Text[ProvinceID].period_name;
    public string Pronunciation => ProvinceText.Text[ProvinceID].pronunciation;
    public string Description => ProvinceText.Text[ProvinceID].description;

    // Province Bonuses & Bonus Text
    public BonusSet ProvinceBonuses => BonusRegistry.Bonuses[ProvinceID];
    public string BonusName => ProvinceBonuses.name;
    public string BonusBenefit1 => ProvinceBonuses.benefit1;
    public string BonusBenefit2 => ProvinceBonuses.benefit2;

    // Variable Data
    public Faction Owner;
    public int HomePopulation;
    public int LeviedPopulation;
    public int Stability;

    // Buildings
    public Farms Farms = new Farms(0);
    public Barracks Barracks = new Barracks(0);
    public Markets Markets = new Markets(0);
    public Port Port = new Port(0);
    public Prestige Prestige = new Prestige(0);

    // Derived
    public int Surplus;
    public float GrowthRate;

    // Contingents
    public List<Contingent> Contingents = new List<Contingent>();
}
