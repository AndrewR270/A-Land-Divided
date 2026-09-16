using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ProvinceData
{
  public string id_province;
  public string owner;

  public int population_home;
  public int population_levied;
  public int stability;

  public BuildingData buildings;

  public List<PendingConstructionData> pending_construction;
  public List<PendingRecruitmentData> pending_recruitment;

  public int replenishment_counter;
  public int unit_id_counter;
}

[System.Serializable]
public class BuildingData
{
  public int farm;
  public int barracks;
  public int market;
  public int port;
  public int prestige;
}


public class Province
{
  // Identifiers
  public string ID { get; private set; }
  public List<string> Adjacent => Adjacency.Lookup[ID];

  // Province Text
  public string Name => ProvinceText.Text[ID].name;
  public string PeriodName => ProvinceText.Text[ID].period_name;
  public string Pronunciation => ProvinceText.Text[ID].pronunciation;
  public string Description => ProvinceText.Text[ID].description;

  // Province Bonuses & Bonus Text
  public BonusSet ProvinceBonuses => Bonuses.BonusSets[ID];
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

  public Province(string id) { ID = id; }
}
