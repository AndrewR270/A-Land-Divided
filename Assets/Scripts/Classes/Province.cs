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

  // Bonuses
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
  public Farms Farms;
  public Barracks Barracks;
  public Markets Markets;
  public Port Port;
  public Prestige Prestige;

  // Derived
  public int Surplus;
  public float GrowthRate;

  // Contingents
  public List<Contingent> Contingents;

  // Queues
  public List<PendingConstruction> PendingConstruction;
  public List<PendingRecruitment> PendingRecruitment;

  public int ReplenishmentCounter;
  public int UnitIDCounter;

  // Constructor used by loader
  public Province(ProvinceData data)
  {
    ID = data.id_province;

    Owner = FactionManager.GetFaction(data.owner);

    HomePopulation = data.population_home;
    LeviedPopulation = data.population_levied;
    Stability = data.stability;

    // Buildings
    Farms = new Farms(data.buildings.farm, GetPendingTime(data, BuildingID.Farms));
    Barracks = new Barracks(data.buildings.barracks, GetPendingTime(data, BuildingID.Barracks));
    Markets = new Markets(data.buildings.market, GetPendingTime(data, BuildingID.Markets));
    Port = new Port(data.buildings.port, GetPendingTime(data, BuildingID.Port));
    Prestige = new Prestige(data.buildings.prestige, GetPendingTime(data, BuildingID.Prestige));

    // Derived
    Surplus = Farms.surplus;
    GrowthRate = Farms.growth;

    // Contingents
    Contingents = new List<Contingent>();

    // Queues
    PendingConstruction = ConvertConstruction(data.pending_construction);
    PendingRecruitment = ConvertRecruitment(data.pending_recruitment);

    ReplenishmentCounter = data.replenishment_counter;
    UnitIDCounter = data.unit_id_counter;
  }

  private int GetPendingTime(ProvinceData data, BuildingID id)
  {
    foreach (var pc in data.pending_construction)
      if (pc.building_type == id.ToString().ToLower())
        return pc.turns_remaining;

    return 0;
  }

  private List<PendingConstruction> ConvertConstruction(List<PendingConstructionData> list)
  {
    var result = new List<PendingConstruction>();
    foreach (var pc in list)
      result.Add(new PendingConstruction(pc));
    return result;
  }

  private List<PendingRecruitment> ConvertRecruitment(List<PendingRecruitmentData> list)
  {
    var result = new List<PendingRecruitment>();
    foreach (var pr in list)
      result.Add(new PendingRecruitment(pr));
    return result;
  }
}
