using UnityEngine;

public struct BuildingText
{
  public string Name;
  public string Description;
  public string Benefit1;
  public string Benefit2;

  public BuildingText(string name, string description, string benefit1, string benefit2)
  {
    Name = name;
    Description = description;
    Benefit1 = benefit1;
    Benefit2 = benefit2;
  }
}

public static class BuildingDescriptions
{
  public static readonly BuildingText Farms = new BuildingText(
    "Farms",
    "Farms increase surplus and population growth.",
    " food surplus",
    " growth per surplus"
  );

  public static readonly BuildingText Barracks = new BuildingText(
    "Barracks",
    "Barracks improve replenishment and levy experience.",
    " turns to replenish a contingent",
    "% chance of experience gain"
  );

  public static readonly BuildingText Markets = new BuildingText(
    "Market",
    "Markets increase trade income and tax revenue.",
    " global trade income bonus",
    " tax revenue per population"
  );

  public static readonly BuildingText Port = new BuildingText(
    "Port",
    "Ports support fleets and reduce ship cost.",
    " fleets supported",
    " cost to build ships"
  );

  
  public static readonly BuildingText Victory = new BuildingText(
    "Victory Building",
    "Counts towards special victory condition.",
    " victory points per turn",
    " bonus to province stability"
  );
}
